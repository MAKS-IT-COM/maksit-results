using Microsoft.AspNetCore.Mvc;


namespace MaksIT.Results.Mvc;

public class StatusCodeResult : IActionResult {
  public int StatusCode { get; }

  public StatusCodeResult(int statusCode) {
    StatusCode = statusCode;
  }

  public async Task ExecuteResultAsync(ActionContext context) {
    context.HttpContext.Response.StatusCode = StatusCode;
    await Task.CompletedTask;
  }
}