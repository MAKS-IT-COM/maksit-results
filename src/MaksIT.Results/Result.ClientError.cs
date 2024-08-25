using System.Net;

namespace MaksIT.Results {

  public partial class Result {

    /// <summary>
    /// Returns a result indicating that the server could not understand the request due to invalid syntax.
    /// Corresponds to HTTP status code 400 Bad Request.
    /// </summary>
    public static Result BadRequest(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Returns a result indicating that the client must authenticate itself to get the requested response.
    /// Corresponds to HTTP status code 401 Unauthorized.
    /// </summary>
    public static Result Unauthorized(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.Unauthorized);
    }

    /// <summary>
    /// Returns a result indicating that the client does not have access rights to the content.
    /// Corresponds to HTTP status code 403 Forbidden.
    /// </summary>
    public static Result Forbidden(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.Forbidden);
    }

    /// <summary>
    /// Returns a result indicating that the server can not find the requested resource.
    /// Corresponds to HTTP status code 404 Not Found.
    /// </summary>
    public static Result NotFound(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
    /// Corresponds to HTTP status code 409 Conflict.
    /// </summary>
    public static Result Conflict(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.Conflict);
    }

    /// <summary>
    /// Returns a result indicating that the requested resource is no longer available and will not be available again.
    /// Corresponds to HTTP status code 410 Gone.
    /// </summary>
    public static Result Gone(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)410); // 410 Gone
    }

    /// <summary>
    /// Returns a result indicating that the request failed because it depended on another request and that request failed.
    /// Corresponds to HTTP status code 424 Failed Dependency.
    /// </summary>
    public static Result FailedDependency(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)424); // 424 Failed Dependency
    }

    /// <summary>
    /// Returns a result indicating that the server requires the request to be conditional.
    /// Corresponds to HTTP status code 428 Precondition Required.
    /// </summary>
    public static Result PreconditionRequired(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)428); // 428 Precondition Required
    }

    /// <summary>
    /// Returns a result indicating that the user has sent too many requests in a given amount of time.
    /// Corresponds to HTTP status code 429 Too Many Requests.
    /// </summary>
    public static Result TooManyRequests(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)429); // 429 Too Many Requests
    }

    /// <summary>
    /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
    /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
    /// </summary>
    public static Result RequestHeaderFieldsTooLarge(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)431); // 431 Request Header Fields Too Large
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request entity because it is too large.
    /// Corresponds to HTTP status code 413 Payload Too Large.
    /// </summary>
    public static Result PayloadTooLarge(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)413); // 413 Payload Too Large
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because the URI is too long.
    /// Corresponds to HTTP status code 414 URI Too Long.
    /// </summary>
    public static Result UriTooLong(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)414); // 414 URI Too Long
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
    /// Corresponds to HTTP status code 415 Unsupported Media Type.
    /// </summary>
    public static Result UnsupportedMediaType(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.UnsupportedMediaType);
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
    /// Corresponds to HTTP status code 411 Length Required.
    /// </summary>
    public static Result LengthRequired(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)411); // 411 Length Required
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
    /// Corresponds to HTTP status code 422 Unprocessable Entity.
    /// </summary>
    public static Result UnprocessableEntity(params string[] messages) {
      return new Result(false, new List<string>(messages), (HttpStatusCode)422); // 422 Unprocessable Entity
    }
  }

  public partial class Result<T> : Result {

    /// <summary>
    /// Returns a result indicating that the server could not understand the request due to invalid syntax.
    /// Corresponds to HTTP status code 400 Bad Request.
    /// </summary>
    public static Result<T> BadRequest(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Returns a result indicating that the client must authenticate itself to get the requested response.
    /// Corresponds to HTTP status code 401 Unauthorized.
    /// </summary>
    public static Result<T> Unauthorized(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.Unauthorized);
    }

    /// <summary>
    /// Returns a result indicating that the client does not have access rights to the content.
    /// Corresponds to HTTP status code 403 Forbidden.
    /// </summary>
    public static Result<T> Forbidden(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.Forbidden);
    }

    /// <summary>
    /// Returns a result indicating that the server can not find the requested resource.
    /// Corresponds to HTTP status code 404 Not Found.
    /// </summary>
    public static Result<T> NotFound(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
    /// Corresponds to HTTP status code 409 Conflict.
    /// </summary>
    public static Result<T> Conflict(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.Conflict);
    }

    /// <summary>
    /// Returns a result indicating that the requested resource is no longer available and will not be available again.
    /// Corresponds to HTTP status code 410 Gone.
    /// </summary>
    public static Result<T> Gone(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)410); // 410 Gone
    }

    /// <summary>
    /// Returns a result indicating that the request failed because it depended on another request and that request failed.
    /// Corresponds to HTTP status code 424 Failed Dependency.
    /// </summary>
    public static Result<T> FailedDependency(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)424); // 424 Failed Dependency
    }

    /// <summary>
    /// Returns a result indicating that the server requires the request to be conditional.
    /// Corresponds to HTTP status code 428 Precondition Required.
    /// </summary>
    public static Result<T> PreconditionRequired(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)428); // 428 Precondition Required
    }

    /// <summary>
    /// Returns a result indicating that the user has sent too many requests in a given amount of time.
    /// Corresponds to HTTP status code 429 Too Many Requests.
    /// </summary>
    public static Result<T> TooManyRequests(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)429); // 429 Too Many Requests
    }

    /// <summary>
    /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
    /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
    /// </summary>
    public static Result<T> RequestHeaderFieldsTooLarge(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)431); // 431 Request Header Fields Too Large
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request entity because it is too large.
    /// Corresponds to HTTP status code 413 Payload Too Large.
    /// </summary>
    public static Result<T> PayloadTooLarge(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)413); // 413 Payload Too Large
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because the URI is too long.
    /// Corresponds to HTTP status code 414 URI Too Long.
    /// </summary>
    public static Result<T> UriTooLong(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)414); // 414 URI Too Long
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
    /// Corresponds to HTTP status code 415 Unsupported Media Type.
    /// </summary>
    public static Result<T> UnsupportedMediaType(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.UnsupportedMediaType);
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
    /// Corresponds to HTTP status code 411 Length Required.
    /// </summary>
    public static Result<T> LengthRequired(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)411); // 411 Length Required
    }

    /// <summary>
    /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
    /// Corresponds to HTTP status code 422 Unprocessable Entity.
    /// </summary>
    public static Result<T> UnprocessableEntity(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), (HttpStatusCode)422); // 422 Unprocessable Entity
    }
  }
}
