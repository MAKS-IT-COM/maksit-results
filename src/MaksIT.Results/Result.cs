using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaksIT.Results;

public partial class Result {
  public bool IsSuccess { get; }
  public List<string> Messages { get; }
  public HttpStatusCode StatusCode { get; }

  protected Result(bool isSuccess, List<string> messages, HttpStatusCode statusCode) {
    IsSuccess = isSuccess;
    Messages = messages ?? new List<string>();
    StatusCode = statusCode;
  }

  /// <summary>
  /// Converts the current Result{T} to a non-generic Result.
  /// </summary>
  /// <returns>A non-generic Result object.</returns>
  public Result ToResult() {
    return new Result(IsSuccess, Messages, StatusCode);
  }

  /// <summary>
  /// Converts this Result into a Result of another type while retaining the same success status, messages, and status code.
  /// </summary>
  /// <typeparam name="U">The target type for the Result.</typeparam>
  /// <param name="value">The new value of type U for the converted Result.</param>
  /// <returns>A Result of type U.</returns>
  public Result<U?> ToResultOfType<U>(U? value) {
    return new Result<U?>(value, IsSuccess, Messages, StatusCode);
  }

  /// <summary>
  /// Converts the current Result to an IActionResult.
  /// </summary>
  /// <returns>IActionResult that represents the HTTP response.</returns>
  public virtual IActionResult ToActionResult() {
    if (IsSuccess) {
      return new StatusCodeResult((int)StatusCode);
    }
    else {
      var problemDetails = new ProblemDetails {
        Status = (int)StatusCode,
        Title = "An error occurred",
        Detail = string.Join("; ", Messages),
        Instance = null // You can customize the instance URI if needed
      };
      return new ObjectResult(problemDetails) { StatusCode = (int)StatusCode };
    }
  }
}

public partial class Result<T> : Result {
  public T? Value { get; }

  public Result(T? value, bool isSuccess, List<string> messages, HttpStatusCode statusCode)
      : base(isSuccess, messages, statusCode) {
    Value = value;
  }

  /// <summary>
  /// Converts this Result<T> to a Result<U> while retaining success status, messages, and status code.
  /// </summary>
  /// <typeparam name="U">The target type for the Result.</typeparam>
  /// <param name="newValueFunc">A function to transform the current value to a new value of type U.</param>
  /// <returns>A Result<U> containing the transformed value.</returns>
  public Result<U?> ToResultOfType<U>(Func<T?, U?> newValueFunc) {
    return new Result<U?>(newValueFunc(Value), IsSuccess, Messages, StatusCode);
  }

  /// <summary>
  /// Converts the current Result<T> to an IActionResult.
  /// </summary>
  /// <returns>IActionResult that represents the HTTP response.</returns>
  public override IActionResult ToActionResult() {
    if (IsSuccess) {
      if (Value is not null) {
        return new ObjectResult(Value) { StatusCode = (int)StatusCode };
      }
      return base.ToActionResult();
    }
    else {
      return base.ToActionResult();
    }
  }
}
