using System.Net;


namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result BadRequest(string message) =>
    BadRequest(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result BadRequest(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result Unauthorized(string message) =>
    Unauthorized(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result Unauthorized(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result Forbidden(string message) =>
    Forbidden(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result Forbidden(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result NotFound(string message) =>
    NotFound(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result NotFound(List<string> messagess) {
    return new Result(false, messagess, HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result Conflict(string message) =>
    Conflict(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result Conflict(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.Conflict);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result Gone(string message) =>
    Gone(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result Gone(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)410); // 410 Gone
  }

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result FailedDependency(string message) =>
    FailedDependency(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result FailedDependency(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)424); // 424 Failed Dependency
  }

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result PreconditionRequired(string message) =>
    PreconditionRequired(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result PreconditionRequired(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)428); // 428 Precondition Required
  }

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result TooManyRequests(string message) =>
    TooManyRequests(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result TooManyRequests(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)429); // 429 Too Many Requests
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result RequestHeaderFieldsTooLarge(string message) =>
    RequestHeaderFieldsTooLarge(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result RequestHeaderFieldsTooLarge(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)431); // 431 Request Header Fields Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result PayloadTooLarge(string message) =>
    PayloadTooLarge(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result PayloadTooLarge(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)413); // 413 Payload Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result UriTooLong(string message) =>
    UriTooLong(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result UriTooLong(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)414); // 414 URI Too Long
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result UnsupportedMediaType(string message) =>
    UnsupportedMediaType(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result UnsupportedMediaType(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.UnsupportedMediaType);
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result LengthRequired(string message) =>
    LengthRequired(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result LengthRequired(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)411); // 411 Length Required
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result UnprocessableEntity(string message) =>
    UnprocessableEntity(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result UnprocessableEntity(List<string> messages) {
    return new Result(false, messages, (HttpStatusCode)422); // 422 Unprocessable Entity
  }
}

public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result<T> BadRequest(T? value, string message) =>
    BadRequest(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result<T> BadRequest(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result<T> Unauthorized(T? value, string message) =>
    Unauthorized(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result<T> Unauthorized(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result<T> Forbidden(T? value, string message) =>
    Forbidden(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result<T> Forbidden(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result<T> NotFound(T? value, string message) =>
    NotFound(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result<T> NotFound(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result<T> Conflict(T? value, string message) =>
    Conflict(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result<T> Conflict(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.Conflict);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result<T> Gone(T? value, string message) =>
    Gone(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result<T> Gone(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)410); // 410 Gone
  }

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result<T> FailedDependency(T? value, string message) =>
    FailedDependency(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result<T> FailedDependency(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)424); // 424 Failed Dependency
  }

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result<T> PreconditionRequired(T? value, string message) =>
    PreconditionRequired(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result<T> PreconditionRequired(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)428); // 428 Precondition Required
  }

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result<T> TooManyRequests(T? value, string message) =>
    TooManyRequests(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result<T> TooManyRequests(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)429); // 429 Too Many Requests
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result<T> RequestHeaderFieldsTooLarge(T? value, string message) =>
    RequestHeaderFieldsTooLarge(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result<T> RequestHeaderFieldsTooLarge(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)431); // 431 Request Header Fields Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result<T> PayloadTooLarge(T? value, string message) =>
    PayloadTooLarge(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result<T> PayloadTooLarge(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)413); // 413 Payload Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result<T> UriTooLong(T? value, string message) =>
    UriTooLong(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result<T> UriTooLong(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)414); // 414 URI Too Long
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result<T> UnsupportedMediaType(T? value, string message) =>
    UnsupportedMediaType(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result<T> UnsupportedMediaType(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.UnsupportedMediaType);
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result<T> LengthRequired(T? value, string message) =>
    LengthRequired(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result<T> LengthRequired(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)411); // 411 Length Required
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result<T> UnprocessableEntity(T? value, string message) =>
    UnprocessableEntity(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result<T> UnprocessableEntity(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, (HttpStatusCode)422); // 422 Unprocessable Entity
  }
}
