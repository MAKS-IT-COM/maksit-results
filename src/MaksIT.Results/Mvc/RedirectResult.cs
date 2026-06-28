using Microsoft.AspNetCore.Mvc;

namespace MaksIT.Results.Mvc;

public class RedirectResult(string location) : IActionResult {
  public string Location { get; } = location;

  public Task ExecuteResultAsync(ActionContext context) {
    context.HttpContext.Response.Redirect(Location);
    return Task.CompletedTask;
  }
}
