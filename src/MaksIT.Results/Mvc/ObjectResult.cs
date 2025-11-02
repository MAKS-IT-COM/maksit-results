using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;


namespace MaksIT.Results.Mvc;

public class ObjectResult(object? value) : IActionResult {
  private static readonly JsonSerializerOptions _jsonSerializerOptions = new() {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };

  public object? Value { get; } = value;
  public int? StatusCode { get; set; }

  public async Task ExecuteResultAsync(ActionContext context) {
    var response = context.HttpContext.Response;

    if (StatusCode.HasValue) {
      response.StatusCode = StatusCode.Value;
    }

    response.ContentType = "application/json";

    if (Value is not null) {
      await JsonSerializer.SerializeAsync(
        response.Body,
        Value,
        Value?.GetType() ?? typeof(object),
        _jsonSerializerOptions
      );
    }
  }
}