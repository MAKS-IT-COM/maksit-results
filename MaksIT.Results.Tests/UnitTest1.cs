using Xunit;
using System.Net;
using MaksIT.Results;
using MaksIT.Results.Mvc;

namespace MaksIT.Results.Tests {
  public class ResultTests {
    [Fact]
    public void Result_Ok_ShouldReturnSuccess() {
      // Arrange
      var message = "Operation successful";

      // Act
      var result = Result.Ok(message);

      // Assert
      Assert.True(result.IsSuccess);
      Assert.Contains(message, result.Messages);
      Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void Result_BadRequest_ShouldReturnFailure() {
      // Arrange
      var message = "Invalid request";

      // Act
      var result = Result.BadRequest(message);

      // Assert
      Assert.False(result.IsSuccess);
      Assert.Contains(message, result.Messages);
      Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public void Result_Generic_Ok_ShouldReturnSuccessWithValue() {
      // Arrange
      var value = 42;
      var message = "Operation successful";

      // Act
      var result = Result<int>.Ok(value, message);

      // Assert
      Assert.True(result.IsSuccess);
      Assert.Equal(value, result.Value);
      Assert.Contains(message, result.Messages);
      Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void Result_Generic_NotFound_ShouldReturnFailureWithNullValue() {
      // Arrange
      var message = "Resource not found";

      // Act
      var result = Result<string>.NotFound(null, message);

      // Assert
      Assert.False(result.IsSuccess);
      Assert.Null(result.Value);
      Assert.Contains(message, result.Messages);
      Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public void Result_ToResultOfType_ShouldTransformValue() {
      // Arrange
      var initialValue = 42;
      var transformedValue = "42";
      var result = Result<int>.Ok(initialValue);

      // Act
      var transformedResult = result.ToResultOfType(value => value.ToString());

      // Assert
      Assert.True(transformedResult.IsSuccess);
      Assert.Equal(transformedValue, transformedResult.Value);
      Assert.Equal(result.StatusCode, transformedResult.StatusCode);
    }

    [Fact]
    public void Result_ToActionResult_ShouldReturnStatusCodeResult() {
      // Arrange
      var result = Result.Ok("Operation successful");

      // Act
      var actionResult = result.ToActionResult();

      // Assert
      Assert.IsType<StatusCodeResult>(actionResult);
      var statusCodeResult = actionResult as StatusCodeResult;
      Assert.NotNull(statusCodeResult);
      Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Result_ToActionResult_ShouldReturnObjectResultForFailure() {
      // Arrange
      var errorMessage = "An error occurred";
      var result = Result.BadRequest(errorMessage);

      // Act
      var actionResult = result.ToActionResult();

      // Assert
      Assert.IsType<ObjectResult>(actionResult);
      var objectResult = actionResult as ObjectResult;
      Assert.NotNull(objectResult);
      Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
      Assert.IsType<ProblemDetails>(objectResult.Value);
      var problemDetails = objectResult.Value as ProblemDetails;
      Assert.NotNull(problemDetails);
      Assert.Equal((int)HttpStatusCode.BadRequest, problemDetails.Status);
      Assert.Equal("An error occurred", problemDetails.Title);
      Assert.Equal(errorMessage, problemDetails.Detail);
    }

    [Fact]
    public void Result_Generic_ToActionResult_ShouldReturnObjectResultWithValue() {
      // Arrange
      var value = new { Id = 1, Name = "Test" };
      var result = Result<object>.Ok(value);

      // Act
      var actionResult = result.ToActionResult();

      // Assert
      Assert.IsType<ObjectResult>(actionResult);
      var objectResult = actionResult as ObjectResult;
      Assert.NotNull(objectResult);
      Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
      Assert.Equal(value, objectResult.Value);
    }
  }
}