using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaksIT.Results.Mvc;

public class ObjectResult : IActionResult {
  public object? Value { get; }
  public int? StatusCode { get; set; }

  public ObjectResult(object? value) {
    Value = value;
  }

  public async Task ExecuteResultAsync(ActionContext context) {
    var response = context.HttpContext.Response;
    if (StatusCode.HasValue) {
      response.StatusCode = StatusCode.Value;
    }
    response.ContentType = "application/json";
    if (Value is not null) {
      await JsonSerializer.SerializeAsync(response.Body, Value);
    }
  }
}