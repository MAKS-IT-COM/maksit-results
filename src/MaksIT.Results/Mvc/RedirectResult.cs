using Microsoft.AspNetCore.Mvc;

namespace MaksIT.Results.Mvc;

/// <summary>
/// HTTP redirect <see cref="IActionResult"/>. Preserves the redirection status code (301/302/303/307/308).
/// </summary>
public class RedirectResult : IActionResult {
  /// <summary>Target URL for the <c>Location</c> header.</summary>
  public string Location { get; }

  /// <summary>HTTP redirection status code.</summary>
  public int StatusCode { get; }

  /// <summary>Creates a 302 Found redirect.</summary>
  public RedirectResult(string location) : this(location, (int)System.Net.HttpStatusCode.Found) {
  }

  /// <summary>Creates a redirect with an explicit status code.</summary>
  public RedirectResult(string location, int statusCode) {
    Location = location;
    StatusCode = statusCode;
  }

  public Task ExecuteResultAsync(ActionContext context) {
    var response = context.HttpContext.Response;
    response.StatusCode = StatusCode;
    response.Headers["Location"] = Location;
    return Task.CompletedTask;
  }
}
