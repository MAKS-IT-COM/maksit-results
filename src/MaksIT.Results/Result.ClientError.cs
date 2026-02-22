using System.Net;


namespace MaksIT.Results;

public partial class Result {

  #region Common Client Errors

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result BadRequest(params string [] messages) {
    return new Result(false, [..messages], HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result Unauthorized(params string [] messages) {
    return new Result(false, [..messages], HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result Forbidden(params string [] messages) {
    return new Result(false, [..messages], HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result NotFound(params string [] messages) {
    return new Result(false, [.. messages], HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result Conflict(params string [] messages) {
    return new Result(false, [..messages], HttpStatusCode.Conflict);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result Gone(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)410); // 410 Gone
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result LengthRequired(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)411); // 411 Length Required
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result PayloadTooLarge(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)413); // 413 Payload Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result UriTooLong(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)414); // 414 URI Too Long
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result UnsupportedMediaType(params string [] messages) {
    return new Result(false, [..messages], HttpStatusCode.UnsupportedMediaType);
  }

  #endregion

  #region Extended Or Less Common Client Errors

  /// <summary>
  /// Returns a result indicating that payment is required to access the requested resource.
  /// Corresponds to HTTP status code 402 Payment Required.
  /// </summary>
  public static Result PaymentRequired(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)402); // 402 Payment Required
  }

  /// <summary>
  /// Returns a result indicating that the request method is known by the server but is not supported by the target resource.
  /// Corresponds to HTTP status code 405 Method Not Allowed.
  /// </summary>
  public static Result MethodNotAllowed(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)405); // 405 Method Not Allowed
  }

  /// <summary>
  /// Returns a result indicating that the server cannot produce a response matching the list of acceptable values defined in the request headers.
  /// Corresponds to HTTP status code 406 Not Acceptable.
  /// </summary>
  public static Result NotAcceptable(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)406); // 406 Not Acceptable
  }

  /// <summary>
  /// Returns a result indicating that authentication with a proxy is required.
  /// Corresponds to HTTP status code 407 Proxy Authentication Required.
  /// </summary>
  public static Result ProxyAuthenticationRequired(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)407); // 407 Proxy Authentication Required
  }

  /// <summary>
  /// Returns a result indicating that the server timed out waiting for the request.
  /// Corresponds to HTTP status code 408 Request Timeout.
  /// </summary>
  public static Result RequestTimeout(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)408); // 408 Request Timeout
  }

  /// <summary>
  /// Returns a result indicating that one or more conditions given in the request header fields evaluated to false.
  /// Corresponds to HTTP status code 412 Precondition Failed.
  /// </summary>
  public static Result PreconditionFailed(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)412); // 412 Precondition Failed
  }

  /// <summary>
  /// Returns a result indicating that the range specified by the Range header field cannot be fulfilled.
  /// Corresponds to HTTP status code 416 Range Not Satisfiable.
  /// </summary>
  public static Result RangeNotSatisfiable(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)416); // 416 Range Not Satisfiable
  }

  /// <summary>
  /// Returns a result indicating that the expectation given in the request's Expect header could not be met.
  /// Corresponds to HTTP status code 417 Expectation Failed.
  /// </summary>
  public static Result ExpectationFailed(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)417); // 417 Expectation Failed
  }

  /// <summary>
  /// Returns a result indicating that the server refuses to brew coffee because it is, permanently, a teapot.
  /// Corresponds to HTTP status code 418 I'm a teapot.
  /// </summary>
  public static Result ImATeapot(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)418); // 418 I'm a teapot
  }

  /// <summary>
  /// Returns a result indicating that the request was directed at a server that is not able to produce a response.
  /// Corresponds to HTTP status code 421 Misdirected Request.
  /// </summary>
  public static Result MisdirectedRequest(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)421); // 421 Misdirected Request
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result UnprocessableEntity(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)422); // 422 Unprocessable Entity
  }

  /// <summary>
  /// Returns a result indicating that access to the target resource is denied because the resource is locked.
  /// Corresponds to HTTP status code 423 Locked.
  /// </summary>
  public static Result Locked(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)423); // 423 Locked
  }

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result FailedDependency(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)424); // 424 Failed Dependency
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to risk processing a request that might be replayed.
  /// Corresponds to HTTP status code 425 Too Early.
  /// </summary>
  public static Result TooEarly(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)425); // 425 Too Early
  }

  /// <summary>
  /// Returns a result indicating that the server refuses to perform the request using the current protocol.
  /// Corresponds to HTTP status code 426 Upgrade Required.
  /// </summary>
  public static Result UpgradeRequired(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)426); // 426 Upgrade Required
  }

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result PreconditionRequired(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)428); // 428 Precondition Required
  }

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result TooManyRequests(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)429); // 429 Too Many Requests
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result RequestHeaderFieldsTooLarge(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)431); // 431 Request Header Fields Too Large
  }

  /// <summary>
  /// Returns a result indicating that access to the requested resource is denied for legal reasons.
  /// Corresponds to HTTP status code 451 Unavailable For Legal Reasons.
  /// </summary>
  public static Result UnavailableForLegalReasons(params string [] messages) {
    return new Result(false, [..messages], (HttpStatusCode)451); // 451 Unavailable For Legal Reasons
  }

  #endregion
}

public partial class Result<T> : Result {

  #region Common Client Errors

  /// <summary>
  /// Returns a result indicating that the server could not understand the request due to invalid syntax.
  /// Corresponds to HTTP status code 400 Bad Request.
  /// </summary>
  public static Result<T> BadRequest(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Returns a result indicating that the client must authenticate itself to get the requested response.
  /// Corresponds to HTTP status code 401 Unauthorized.
  /// </summary>
  public static Result<T> Unauthorized(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Returns a result indicating that the client does not have access rights to the content.
  /// Corresponds to HTTP status code 403 Forbidden.
  /// </summary>
  public static Result<T> Forbidden(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Returns a result indicating that the server can not find the requested resource.
  /// Corresponds to HTTP status code 404 Not Found.
  /// </summary>
  public static Result<T> NotFound(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Returns a result indicating that the request could not be completed due to a conflict with the current state of the resource.
  /// Corresponds to HTTP status code 409 Conflict.
  /// </summary>
  public static Result<T> Conflict(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.Conflict);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource is no longer available and will not be available again.
  /// Corresponds to HTTP status code 410 Gone.
  /// </summary>
  public static Result<T> Gone(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)410); // 410 Gone
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because it expects the request to have a defined Content-Length header.
  /// Corresponds to HTTP status code 411 Length Required.
  /// </summary>
  public static Result<T> LengthRequired(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)411); // 411 Length Required
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request entity because it is too large.
  /// Corresponds to HTTP status code 413 Payload Too Large.
  /// </summary>
  public static Result<T> PayloadTooLarge(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)413); // 413 Payload Too Large
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the URI is too long.
  /// Corresponds to HTTP status code 414 URI Too Long.
  /// </summary>
  public static Result<T> UriTooLong(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)414); // 414 URI Too Long
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request because the media type is unsupported.
  /// Corresponds to HTTP status code 415 Unsupported Media Type.
  /// </summary>
  public static Result<T> UnsupportedMediaType(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], HttpStatusCode.UnsupportedMediaType);
  }

  #endregion

  #region Extended Or Less Common Client Errors

  /// <summary>
  /// Returns a result indicating that payment is required to access the requested resource.
  /// Corresponds to HTTP status code 402 Payment Required.
  /// </summary>
  public static Result<T> PaymentRequired(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)402); // 402 Payment Required
  }

  /// <summary>
  /// Returns a result indicating that the request method is known by the server but is not supported by the target resource.
  /// Corresponds to HTTP status code 405 Method Not Allowed.
  /// </summary>
  public static Result<T> MethodNotAllowed(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)405); // 405 Method Not Allowed
  }

  /// <summary>
  /// Returns a result indicating that the server cannot produce a response matching the list of acceptable values defined in the request headers.
  /// Corresponds to HTTP status code 406 Not Acceptable.
  /// </summary>
  public static Result<T> NotAcceptable(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)406); // 406 Not Acceptable
  }

  /// <summary>
  /// Returns a result indicating that authentication with a proxy is required.
  /// Corresponds to HTTP status code 407 Proxy Authentication Required.
  /// </summary>
  public static Result<T> ProxyAuthenticationRequired(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)407); // 407 Proxy Authentication Required
  }

  /// <summary>
  /// Returns a result indicating that the server timed out waiting for the request.
  /// Corresponds to HTTP status code 408 Request Timeout.
  /// </summary>
  public static Result<T> RequestTimeout(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)408); // 408 Request Timeout
  }

  /// <summary>
  /// Returns a result indicating that one or more conditions given in the request header fields evaluated to false.
  /// Corresponds to HTTP status code 412 Precondition Failed.
  /// </summary>
  public static Result<T> PreconditionFailed(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)412); // 412 Precondition Failed
  }

  /// <summary>
  /// Returns a result indicating that the range specified by the Range header field cannot be fulfilled.
  /// Corresponds to HTTP status code 416 Range Not Satisfiable.
  /// </summary>
  public static Result<T> RangeNotSatisfiable(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)416); // 416 Range Not Satisfiable
  }

  /// <summary>
  /// Returns a result indicating that the expectation given in the request's Expect header could not be met.
  /// Corresponds to HTTP status code 417 Expectation Failed.
  /// </summary>
  public static Result<T> ExpectationFailed(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)417); // 417 Expectation Failed
  }

  /// <summary>
  /// Returns a result indicating that the server refuses to brew coffee because it is, permanently, a teapot.
  /// Corresponds to HTTP status code 418 I'm a teapot.
  /// </summary>
  public static Result<T> ImATeapot(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)418); // 418 I'm a teapot
  }

  /// <summary>
  /// Returns a result indicating that the request was directed at a server that is not able to produce a response.
  /// Corresponds to HTTP status code 421 Misdirected Request.
  /// </summary>
  public static Result<T> MisdirectedRequest(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)421); // 421 Misdirected Request
  }

  /// <summary>
  /// Returns a result indicating that the server cannot process the request due to an illegal request entity.
  /// Corresponds to HTTP status code 422 Unprocessable Entity.
  /// </summary>
  public static Result<T> UnprocessableEntity(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)422); // 422 Unprocessable Entity
  }

  /// <summary>
  /// Returns a result indicating that access to the target resource is denied because the resource is locked.
  /// Corresponds to HTTP status code 423 Locked.
  /// </summary>
  public static Result<T> Locked(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)423); // 423 Locked
  }

  /// <summary>
  /// Returns a result indicating that the request failed because it depended on another request and that request failed.
  /// Corresponds to HTTP status code 424 Failed Dependency.
  /// </summary>
  public static Result<T> FailedDependency(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)424); // 424 Failed Dependency
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to risk processing a request that might be replayed.
  /// Corresponds to HTTP status code 425 Too Early.
  /// </summary>
  public static Result<T> TooEarly(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)425); // 425 Too Early
  }

  /// <summary>
  /// Returns a result indicating that the server refuses to perform the request using the current protocol.
  /// Corresponds to HTTP status code 426 Upgrade Required.
  /// </summary>
  public static Result<T> UpgradeRequired(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)426); // 426 Upgrade Required
  }

  /// <summary>
  /// Returns a result indicating that the server requires the request to be conditional.
  /// Corresponds to HTTP status code 428 Precondition Required.
  /// </summary>
  public static Result<T> PreconditionRequired(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)428); // 428 Precondition Required
  }

  /// <summary>
  /// Returns a result indicating that the user has sent too many requests in a given amount of time.
  /// Corresponds to HTTP status code 429 Too Many Requests.
  /// </summary>
  public static Result<T> TooManyRequests(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)429); // 429 Too Many Requests
  }

  /// <summary>
  /// Returns a result indicating that the server is unwilling to process the request because its header fields are too large.
  /// Corresponds to HTTP status code 431 Request Header Fields Too Large.
  /// </summary>
  public static Result<T> RequestHeaderFieldsTooLarge(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)431); // 431 Request Header Fields Too Large
  }

  /// <summary>
  /// Returns a result indicating that access to the requested resource is denied for legal reasons.
  /// Corresponds to HTTP status code 451 Unavailable For Legal Reasons.
  /// </summary>
  public static Result<T> UnavailableForLegalReasons(T? value, params string [] messages) {
    return new Result<T>(value, false, [..messages], (HttpStatusCode)451); // 451 Unavailable For Legal Reasons
  }

  #endregion
}
