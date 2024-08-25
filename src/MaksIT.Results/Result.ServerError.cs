using System.Net;

namespace MaksIT.Results {

  public partial class Result {

    /// <summary>
    /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
    /// Corresponds to HTTP status code 500 Internal Server Error.
    /// </summary>
    public static Result InternalServerError(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Returns a result indicating the server does not support the functionality required to fulfill the request.
    /// Corresponds to HTTP status code 501 Not Implemented.
    /// </summary>
    public static Result NotImplemented(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
    /// Corresponds to HTTP status code 502 Bad Gateway.
    /// </summary>
    public static Result BadGateway(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.BadGateway);
    }

    /// <summary>
    /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
    /// Corresponds to HTTP status code 503 Service Unavailable.
    /// </summary>
    public static Result ServiceUnavailable(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.ServiceUnavailable);
    }

    /// <summary>
    /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
    /// Corresponds to HTTP status code 504 Gateway Timeout.
    /// </summary>
    public static Result GatewayTimeout(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.GatewayTimeout);
    }

    /// <summary>
    /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
    /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
    /// </summary>
    public static Result HttpVersionNotSupported(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.HttpVersionNotSupported);
    }

    /// <summary>
    /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
    /// Corresponds to HTTP status code 506 Variant Also Negotiates.
    /// </summary>
    public static Result VariantAlsoNegotiates(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.VariantAlsoNegotiates);
    }

    /// <summary>
    /// Returns a result indicating the server is unable to store the representation needed to complete the request.
    /// Corresponds to HTTP status code 507 Insufficient Storage.
    /// </summary>
    public static Result InsufficientStorage(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.InsufficientStorage);
    }

    /// <summary>
    /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
    /// Corresponds to HTTP status code 508 Loop Detected.
    /// </summary>
    public static Result LoopDetected(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.LoopDetected);
    }

    /// <summary>
    /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
    /// Corresponds to HTTP status code 510 Not Extended.
    /// </summary>
    public static Result NotExtended(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.NotExtended);
    }

    /// <summary>
    /// Returns a result indicating the client needs to authenticate to gain network access.
    /// Corresponds to HTTP status code 511 Network Authentication Required.
    /// </summary>
    public static Result NetworkAuthenticationRequired(params string[] messages) {
      return new Result(false, new List<string>(messages), HttpStatusCode.NetworkAuthenticationRequired);
    }
  }

  public partial class Result<T> : Result {

    /// <summary>
    /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
    /// Corresponds to HTTP status code 500 Internal Server Error.
    /// </summary>
    public static Result<T> InternalServerError(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Returns a result indicating the server does not support the functionality required to fulfill the request.
    /// Corresponds to HTTP status code 501 Not Implemented.
    /// </summary>
    public static Result<T> NotImplemented(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
    /// Corresponds to HTTP status code 502 Bad Gateway.
    /// </summary>
    public static Result<T> BadGateway(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.BadGateway);
    }

    /// <summary>
    /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
    /// Corresponds to HTTP status code 503 Service Unavailable.
    /// </summary>
    public static Result<T> ServiceUnavailable(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.ServiceUnavailable);
    }

    /// <summary>
    /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
    /// Corresponds to HTTP status code 504 Gateway Timeout.
    /// </summary>
    public static Result<T> GatewayTimeout(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.GatewayTimeout);
    }

    /// <summary>
    /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
    /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
    /// </summary>
    public static Result<T> HttpVersionNotSupported(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.HttpVersionNotSupported);
    }

    /// <summary>
    /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
    /// Corresponds to HTTP status code 506 Variant Also Negotiates.
    /// </summary>
    public static Result<T> VariantAlsoNegotiates(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.VariantAlsoNegotiates);
    }

    /// <summary>
    /// Returns a result indicating the server is unable to store the representation needed to complete the request.
    /// Corresponds to HTTP status code 507 Insufficient Storage.
    /// </summary>
    public static Result<T> InsufficientStorage(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.InsufficientStorage);
    }

    /// <summary>
    /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
    /// Corresponds to HTTP status code 508 Loop Detected.
    /// </summary>
    public static Result<T> LoopDetected(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.LoopDetected);
    }

    /// <summary>
    /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
    /// Corresponds to HTTP status code 510 Not Extended.
    /// </summary>
    public static Result<T> NotExtended(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.NotExtended);
    }

    /// <summary>
    /// Returns a result indicating the client needs to authenticate to gain network access.
    /// Corresponds to HTTP status code 511 Network Authentication Required.
    /// </summary>
    public static Result<T> NetworkAuthenticationRequired(T? value, params string[] messages) {
      return new Result<T>(value, false, new List<string>(messages), HttpStatusCode.NetworkAuthenticationRequired);
    }
  }
}
