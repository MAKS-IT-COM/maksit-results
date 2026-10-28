using System.Net;
using System.Text;
using System.Text.Json;
using MaksIT.Results.Mvc;

namespace MaksIT.Results.Tests;

public class ResultFromHttpResponseTests {
  [Fact]
  public async Task FromHttpResponseAsync_WhenOk_ReturnsSuccess() {
    using var response = new HttpResponseMessage(HttpStatusCode.OK);

    var result = await Result.FromHttpResponseAsync(response, TestContext.Current.CancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
  }

  [Fact]
  public async Task FromHttpResponseAsync_WhenProblemDetails_ReturnsFailureMessages() {
    var problem = new ProblemDetails {
      Status = 404,
      Title = "An error occurred",
      Detail = "Missing; Gone"
    };
    
    using var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
      Content = new StringContent(JsonSerializer.Serialize(problem), Encoding.UTF8, "application/problem+json")
    };

    var result = await Result.FromHttpResponseAsync(response, TestContext.Current.CancellationToken);

    Assert.False(result.IsSuccess);
    Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    Assert.Equal(["Missing", "Gone"], result.Messages);
  }

  [Fact]
  public async Task FromHttpResponseAsync_WhenJsonBody_DeserializesValue() {
    using var response = new HttpResponseMessage(HttpStatusCode.OK) {
      Content = new StringContent("""{"id":3,"name":"hub"}""", Encoding.UTF8, "application/json")
    };

    var result = await Result<SampleDto>.FromHttpResponseAsync(response, TestContext.Current.CancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(3, result.Value!.Id);
    Assert.Equal("hub", result.Value.Name);
  }

  [Fact]
  public async Task FromHttpResponseAsync_WhenRedirect_ReturnsLocation() {
    using var response = new HttpResponseMessage(HttpStatusCode.Found);
    response.Headers.Location = new Uri("https://accounts.example.com/auth");

    var result = await Result<string>.FromHttpResponseAsync(response, TestContext.Current.CancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(HttpStatusCode.Found, result.StatusCode);
    Assert.Equal("https://accounts.example.com/auth", result.Value);
  }

  [Fact]
  public async Task FromHttpResponseAsync_WhenDetailOnlyJson_ParsesWithoutProblemMediaType() {
    using var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
      Content = new StringContent("""{"detail":"Invalid or expired code."}""", Encoding.UTF8, "application/json")
    };

    var result = await Result<string>.FromHttpResponseAsync(response, TestContext.Current.CancellationToken);

    Assert.False(result.IsSuccess);
    Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    Assert.Contains("Invalid or expired code.", result.Messages[0]);
  }

  private sealed record SampleDto(int Id, string Name);
}
