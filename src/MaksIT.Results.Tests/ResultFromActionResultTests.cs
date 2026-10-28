using System.Net;
using MaksIT.Results.Mvc;

namespace MaksIT.Results.Tests;

public class ResultFromActionResultTests {
  [Fact]
  public void FromActionResult_RoundTripsSuccessStatusCode() {
    var original = Result.Ok("done");
    var action = original.ToActionResult();

    var parsed = Result.FromActionResult(action);

    Assert.True(parsed.IsSuccess);
    Assert.Equal(HttpStatusCode.OK, parsed.StatusCode);
  }

  [Fact]
  public void FromActionResult_RoundTripsFailureProblemDetails() {
    var original = Result.BadRequest("Validation failed", "Field required");
    var action = original.ToActionResult();

    var parsed = Result.FromActionResult(action);

    Assert.False(parsed.IsSuccess);
    Assert.Equal(HttpStatusCode.BadRequest, parsed.StatusCode);
    Assert.Equal(original.Messages, parsed.Messages);
  }

  [Fact]
  public void FromActionResult_RoundTripsGenericValue() {
    var dto = new SampleDto(7, "vault");
    var original = Result<SampleDto>.Ok(dto);
    var action = original.ToActionResult();

    var parsed = Result<SampleDto>.FromActionResult(action);

    Assert.True(parsed.IsSuccess);
    Assert.Equal(HttpStatusCode.OK, parsed.StatusCode);
    Assert.NotNull(parsed.Value);
    Assert.Equal(dto.Id, parsed.Value!.Id);
    Assert.Equal(dto.Name, parsed.Value.Name);
  }

  [Fact]
  public void FromActionResult_RoundTripsRedirectWithStatus() {
    var original = Result<string>.MovedPermanently("https://example.com/new");
    var action = original.ToActionResult();

    var redirect = Assert.IsType<RedirectResult>(action);
    Assert.Equal((int)HttpStatusCode.MovedPermanently, redirect.StatusCode);

    var parsed = Result<string>.FromActionResult(action);

    Assert.True(parsed.IsSuccess);
    Assert.Equal(HttpStatusCode.MovedPermanently, parsed.StatusCode);
    Assert.Equal("https://example.com/new", parsed.Value);
  }

  private sealed record SampleDto(int Id, string Name);
}
