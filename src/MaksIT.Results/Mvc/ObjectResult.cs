using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MaksIT.Results.Mvc;

public class ObjectResult(object? value) : IActionResult {
  private static readonly JsonSerializerOptions _jsonSerializerOptions = new() {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };

  public object? Value { get; } = value;
  public int? StatusCode { get; set; }

  public async Task ExecuteResultAsync(ActionContext context) {
    var response = context.HttpContext.Response;
    var jsonOptions = ResolveJsonSerializerOptions(context.HttpContext.RequestServices);

    if (StatusCode.HasValue) {
      response.StatusCode = StatusCode.Value;
    }

    // Set content type based on value type
    if (Value is ProblemDetails) {
      response.ContentType = "application/problem+json";
    }
    else {
      response.ContentType = "application/json";
    }

    if (Value is not null) {
      await JsonSerializer.SerializeAsync(
        response.Body,
        Value,
        Value?.GetType() ?? typeof(object),
        jsonOptions
      );
    }
  }

  /// <summary>
  /// Ignore unconfigured <see cref="JsonOptions"/> from options DI (Pascal case defaults).
  /// Use camel case unless the host set <see cref="JsonSerializerOptions.PropertyNamingPolicy"/>.
  /// </summary>
  private static JsonSerializerOptions ResolveJsonSerializerOptions(IServiceProvider? services) {
    var configured = services?.GetService<IOptions<JsonOptions>>()?.Value?.JsonSerializerOptions;
    if (configured?.PropertyNamingPolicy is not null)
      return configured;

    return _jsonSerializerOptions;
  }
}