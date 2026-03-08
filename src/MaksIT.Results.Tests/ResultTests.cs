using System.Net;

namespace MaksIT.Results.Tests;

public class ResultTests {
  [Fact]
  public void Ok_ShouldReturnSuccess() {
    var message = "Operation successful";

    var result = Result.Ok(message);

    Assert.True(result.IsSuccess);
    Assert.Contains(message, result.Messages);
    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
  }

  [Fact]
  public void BadRequest_ShouldReturnFailure() {
    var message = "Invalid request";

    var result = Result.BadRequest(message);

    Assert.False(result.IsSuccess);
    Assert.Contains(message, result.Messages);
    Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
  }

  [Fact]
  public void Generic_Ok_ShouldReturnSuccessWithValue() {
    var value = 42;
    var message = "Operation successful";

    var result = Result<int>.Ok(value, message);

    Assert.True(result.IsSuccess);
    Assert.Equal(value, result.Value);
    Assert.Contains(message, result.Messages);
    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
  }

  [Fact]
  public void Generic_NotFound_ShouldReturnFailureWithNullValue() {
    var message = "Resource not found";

    var result = Result<string>.NotFound(null, message);

    Assert.False(result.IsSuccess);
    Assert.Null(result.Value);
    Assert.Contains(message, result.Messages);
    Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
  }

  [Fact]
  public void ToResultOfType_ShouldTransformValue() {
    var initialValue = 42;
    var transformedValue = "42";
    var result = Result<int>.Ok(initialValue);

    var transformedResult = result.ToResultOfType(value => value.ToString());

    Assert.True(transformedResult.IsSuccess);
    Assert.Equal(transformedValue, transformedResult.Value);
    Assert.Equal(result.StatusCode, transformedResult.StatusCode);
  }
}
