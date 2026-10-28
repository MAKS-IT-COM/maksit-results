using System.Net;
using MaksIT.Results.Mvc;

namespace MaksIT.Results.Tests;

public class ResultToActionResultTests {
  [Fact]
  public void ToActionResult_WhenSuccess_ReturnsStatusCodeResult() {
    var result = Result.Ok("Operation successful");

    var actionResult = result.ToActionResult();

    var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult);

    Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
  }

  [Fact]
  public void ToActionResult_WhenFailure_ReturnsObjectResultWithProblemDetails() {
    var errorMessage = "An error occurred";
    var result = Result.BadRequest(errorMessage);

    var actionResult = result.ToActionResult();

    var objectResult = Assert.IsType<ObjectResult>(actionResult);

    Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);

    var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);

    Assert.Equal((int)HttpStatusCode.BadRequest, problemDetails.Status);
    Assert.Equal("An error occurred", problemDetails.Title);
    Assert.Equal(errorMessage, problemDetails.Detail);
  }

  [Fact]
  public void ToActionResult_WhenGenericSuccessWithValue_ReturnsObjectResultWithValue() {
    var value = new { Id = 1, Name = "Test" };
    var result = Result<object>.Ok(value);

    var actionResult = result.ToActionResult();

    var objectResult = Assert.IsType<ObjectResult>(actionResult);

    Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
    Assert.Equal(value, objectResult.Value);
  }

  [Fact]
  public void ToActionResult_WhenGenericSuccessWithRedirect_ReturnsRedirectResult() {
    var result = Result<string>.Found("https://example.com/continue");

    var actionResult = result.ToActionResult();

    var redirectResult = Assert.IsType<RedirectResult>(actionResult);

    Assert.Equal("https://example.com/continue", redirectResult.Location);
    Assert.Equal((int)HttpStatusCode.Found, redirectResult.StatusCode);
  }
}
