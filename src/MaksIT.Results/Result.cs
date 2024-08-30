using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaksIT.Results {

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

    protected Result(T? value, bool isSuccess, List<string> messages, HttpStatusCode statusCode)
        : base(isSuccess, messages, statusCode) {
      Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Result{U}"/> by applying a transformation function to the current value.
    /// </summary>
    /// <typeparam name="U">The type of the new value.</typeparam>
    /// <param name="newValueFunc">A function that transforms the current value to the new value, which can be null.</param>
    /// <returns>A new <see cref="Result{U}"/> object containing the transformed value, along with the original success status, messages, and status code.</returns>
    public Result<U?> WithNewValue<U>(Func<T?, U?> newValueFunc) {
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
}
