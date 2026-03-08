using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MaksIT.Results.Mvc;

namespace MaksIT.Results.Tests.Mvc;

public class ObjectResultTests {
  [Fact]
  public async Task ExecuteResultAsync_SerializesToCamelCaseJson() {
    var testObject = new TestPascalCase { FirstName = "John", LastName = "Doe" };
    var objectResult = new ObjectResult(testObject);
    var context = new DefaultHttpContext();
    var memoryStream = new MemoryStream();
    context.Response.Body = memoryStream;
    var actionContext = new ActionContext { HttpContext = context };

    await objectResult.ExecuteResultAsync(actionContext);

    memoryStream.Seek(0, SeekOrigin.Begin);
    var json = await new StreamReader(memoryStream).ReadToEndAsync(TestContext.Current.CancellationToken);
    Assert.Contains("\"firstName\"", json);
    Assert.Contains("\"lastName\"", json);
    Assert.DoesNotContain("\"FirstName\"", json);
    Assert.DoesNotContain("\"LastName\"", json);
  }

  [Fact]
  public async Task ExecuteResultAsync_WhenJsonOptionsWhenWritingNull_OmitsNullProperties() {
    var services = new ServiceCollection();
    services.AddOptions<JsonOptions>().Configure(o => {
      o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
    var serviceProvider = services.BuildServiceProvider();

    var testObject = new TestWithNulls { Id = 1, Name = "Test", Optional = null };
    var objectResult = new ObjectResult(testObject);
    var context = new DefaultHttpContext { RequestServices = serviceProvider };
    var memoryStream = new MemoryStream();
    context.Response.Body = memoryStream;
    var actionContext = new ActionContext { HttpContext = context };

    await objectResult.ExecuteResultAsync(actionContext);

    memoryStream.Seek(0, SeekOrigin.Begin);
    var json = await new StreamReader(memoryStream).ReadToEndAsync(TestContext.Current.CancellationToken);
    Assert.Contains("\"id\"", json);
    Assert.Contains("\"name\"", json);
    Assert.Contains("\"Test\"", json);
    Assert.DoesNotContain("\"optional\"", json);
  }

  private class TestPascalCase {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
  }

  private class TestWithNulls {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Optional { get; set; }
  }
}
