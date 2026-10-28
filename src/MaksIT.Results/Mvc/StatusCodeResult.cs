using Microsoft.AspNetCore.Mvc;

namespace MaksIT.Results.Mvc;

public class StatusCodeResult(int statusCode) : IActionResult {
  public int StatusCode { get; } = statusCode;

  public async Task ExecuteResultAsync(ActionContext context) {
    context.HttpContext.Response.StatusCode = StatusCode;
    await Task.CompletedTask;
  }
}
