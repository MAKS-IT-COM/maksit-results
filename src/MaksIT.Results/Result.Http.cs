using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MaksIT.Core.Extensions;
using MaksIT.Results.Mvc;

namespace MaksIT.Results;

public partial class Result {
  /// <summary>
  /// Reconstructs a <see cref="Result"/> from an <see cref="IActionResult"/> produced by <see cref="ToActionResult"/>.
  /// </summary>
  public static Result FromActionResult(IActionResult actionResult) {
    ArgumentNullException.ThrowIfNull(actionResult);

    switch (actionResult) {
      case StatusCodeResult statusCodeResult:
        return new (true, [], (HttpStatusCode)statusCodeResult.StatusCode);

      case RedirectResult redirectResult:
        return new (true, [redirectResult.Location], (HttpStatusCode)redirectResult.StatusCode);

      case ObjectResult { Value: ProblemDetails problem } objectResult:
        return FromProblemDetails(problem, objectResult.StatusCode);

      case ObjectResult objectResult:
        var status = (HttpStatusCode)(objectResult.StatusCode ?? (int)HttpStatusCode.OK);
        if (IsSuccessStatusCode(status))
          return new (true, [], status);

        return new (false, ["Response indicated failure without ProblemDetails."], status);

      default:
        return InternalServerError($"Unsupported IActionResult type: {actionResult.GetType().FullName}.");
    }
  }

  /// <summary>
  /// Reconstructs a <see cref="Result"/> from an HTTP response written in the <see cref="ToActionResult"/> wire shape
  /// (status-only success, <c>application/problem+json</c> failures).
  /// </summary>
  public static Task<Result> FromHttpResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken = default) {
    ArgumentNullException.ThrowIfNull(response);
    return FromHttpResponseCoreAsync(response, cancellationToken);
  }

  private static async Task<Result> FromHttpResponseCoreAsync(HttpResponseMessage response, CancellationToken cancellationToken) {
    var status = response.StatusCode;

    if (IsRedirectionStatusCode(status)) {
      var location = response.Headers.Location?.ToString();
      if (!string.IsNullOrWhiteSpace(location))
        return new (true, [location], status);

      return new (false, ["Redirect response missing Location header."], status);
    }

    if (IsSuccessStatusCode(status))
      return new (true, [], status);

    return await FailFromHttpContentAsync(response, status, cancellationToken).ConfigureAwait(false);
  }

  internal static bool IsRedirectionStatusCode(HttpStatusCode statusCode) =>
    statusCode is HttpStatusCode.MultipleChoices
      or HttpStatusCode.MovedPermanently
      or HttpStatusCode.Found
      or HttpStatusCode.SeeOther
      or HttpStatusCode.TemporaryRedirect
      or HttpStatusCode.PermanentRedirect;

  internal static bool IsSuccessStatusCode(HttpStatusCode statusCode) {
    var code = (int)statusCode;
    return code is >= 200 and <= 299;
  }

  internal static Result Create(bool isSuccess, List<string> messages, HttpStatusCode statusCode) =>
    new(isSuccess, messages, statusCode);

  internal static Result FromProblemDetails(ProblemDetails problem, int? statusCodeOverride) {
    var status = (HttpStatusCode)(statusCodeOverride ?? problem.Status ?? (int)HttpStatusCode.InternalServerError);
    var messages = ReadProblemMessages(problem);
    return Create(false, messages, status);
  }

  internal static List<string> ReadProblemMessages(ProblemDetails problem) {
    if (!string.IsNullOrWhiteSpace(problem.Detail)) {
      var parts = problem.Detail.Split("; ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
      if (parts.Length > 0)
        return [.. parts];

      return [problem.Detail];
    }

    if (!string.IsNullOrWhiteSpace(problem.Title))
      return [problem.Title];

    return ["An error occurred"];
  }

  internal static async Task<Result> FailFromHttpContentAsync(
      HttpResponseMessage response,
      HttpStatusCode status,
      CancellationToken cancellationToken) {
    var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

    if (TryReadProblemDetails(response.Content.Headers.ContentType?.MediaType, body, out var problem))
      return FromProblemDetails(problem, (int)status);

    if (string.IsNullOrWhiteSpace(body))
      return new (false, [$"HTTP {(int)status}."], status);

    return new (false, [Truncate($"HTTP {(int)status}. Response: {body}")], status);
  }

  internal static bool TryReadProblemDetails(string? mediaType, string body, out ProblemDetails problem) {
    problem = null!;
    if (string.IsNullOrWhiteSpace(body))
      return false;

    var isProblemJson = mediaType is not null
      && mediaType.Equals("application/problem+json", StringComparison.OrdinalIgnoreCase);

    if (!isProblemJson && !LooksLikeJsonObject(body))
      return false;

    try {
      var parsed = body.ToObject<ProblemDetails>();
      if (parsed is null)
        return false;

      if (string.IsNullOrWhiteSpace(parsed.Detail) && string.IsNullOrWhiteSpace(parsed.Title) && parsed.Status is null)
        return false;

      problem = parsed;
      return true;
    }
    catch (JsonException) {
      return false;
    }
  }

  internal static bool LooksLikeJsonObject(string body) {
    var trimmed = body.AsSpan().TrimStart();
    return trimmed.Length > 0 && trimmed[0] == '{';
  }

  internal static string Truncate(string value) {
    const int maxLen = 500;
    return value.Length <= maxLen ? value : value[..maxLen] + "…";
  }
}

public partial class Result<T> {
  /// <summary>
  /// Reconstructs a <see cref="Result{T}"/> from an <see cref="IActionResult"/> produced by <see cref="ToActionResult"/>.
  /// </summary>
  public new static Result<T> FromActionResult(IActionResult actionResult) {
    ArgumentNullException.ThrowIfNull(actionResult);

    switch (actionResult) {
      case RedirectResult redirectResult when typeof(T) == typeof(string):
        return new ((T)(object)redirectResult.Location, true, [], (HttpStatusCode)redirectResult.StatusCode);

      case RedirectResult redirectResult:
        return new (default, false, ["Redirect Location cannot be mapped to the requested result type."], (HttpStatusCode)redirectResult.StatusCode);

      case StatusCodeResult statusCodeResult:
        return new (default, true, [], (HttpStatusCode)statusCodeResult.StatusCode);

      case ObjectResult { Value: ProblemDetails problem } objectResult:
        var failed = Result.FromProblemDetails(problem, objectResult.StatusCode);
        return new (default, false, failed.Messages, failed.StatusCode);

      case ObjectResult { Value: T value } objectResult:
        return new (value, true, [], (HttpStatusCode)(objectResult.StatusCode ?? (int)HttpStatusCode.OK));

      case ObjectResult { Value: not null } objectResult:
        var status = (HttpStatusCode)(objectResult.StatusCode ?? (int)HttpStatusCode.OK);
        if (!Result.IsSuccessStatusCode(status)) {
          var failure = Result.Create(false, ["Response indicated failure without ProblemDetails."], status);
          return new (default, false, failure.Messages, failure.StatusCode);
        }

        try {
          var converted = objectResult.Value.ToJson().ToObject<T>();
          return new (converted, true, [], status);
        }
        catch (JsonException ex) {
          return new (default, false, [$"Could not map ObjectResult value to {typeof(T).Name}: {ex.Message}"], status);
        }

      case ObjectResult objectResult:
        return new (default, true, [], (HttpStatusCode)(objectResult.StatusCode ?? (int)HttpStatusCode.OK));

      default:
        return new (default, false, [$"Unsupported IActionResult type: {actionResult.GetType().FullName}."], HttpStatusCode.InternalServerError);
    }
  }

  /// <summary>
  /// Reconstructs a <see cref="Result{T}"/> from an HTTP response written in the <see cref="Result.ToActionResult"/> wire shape
  /// (JSON body, redirect <c>Location</c>, or <c>application/problem+json</c>).
  /// </summary>
  public new static Task<Result<T>> FromHttpResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken = default) {
    ArgumentNullException.ThrowIfNull(response);
    return FromHttpResponseCoreAsync(response, cancellationToken);
  }

  private static async Task<Result<T>> FromHttpResponseCoreAsync(HttpResponseMessage response, CancellationToken cancellationToken) {
    var status = response.StatusCode;

    if (Result.IsRedirectionStatusCode(status)) {
      var location = response.Headers.Location?.ToString();
      if (typeof(T) == typeof(string) && !string.IsNullOrWhiteSpace(location))
        return new ((T)(object)location, true, [], status);

      return new (default, false, [
        string.IsNullOrWhiteSpace(location)
          ? "Redirect response missing Location header."
          : "Redirect Location cannot be mapped to the requested result type."
      ], status);
    }

    if (!Result.IsSuccessStatusCode(status)) {
      var failed = await Result.FailFromHttpContentAsync(response, status, cancellationToken).ConfigureAwait(false);
      return new (default, false, failed.Messages, failed.StatusCode);
    }

    if (typeof(T) == typeof(object))
      return new (default, true, [], status);

    var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

    if (typeof(T) == typeof(string)) {
      if (string.IsNullOrWhiteSpace(body))
        return new (default, false, ["Response body was empty."], status);

      return new ((T)(object)body, true, [], status);
    }

    if (string.IsNullOrWhiteSpace(body))
      return new (default, true, [], status);

    try {
      var value = body.ToObject<T>();
      return new (value, true, [], status);
    }
    catch (JsonException ex) {
      return new (default, false, [$"Could not deserialize response as {typeof(T).Name}: {ex.Message}"], status);
    }
  }
}
