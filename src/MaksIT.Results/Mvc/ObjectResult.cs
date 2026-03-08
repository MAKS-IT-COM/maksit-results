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

    // Prefer app-configured JSON options (from AddJsonOptions), fall back to default
    var jsonOptions = context.HttpContext.RequestServices?.GetService<IOptions<JsonOptions>>()?.Value?.JsonSerializerOptions
      ?? _jsonSerializerOptions;

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
}