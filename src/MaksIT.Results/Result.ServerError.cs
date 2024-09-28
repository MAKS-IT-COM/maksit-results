using System.Net;

namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
  /// Corresponds to HTTP status code 500 Internal Server Error.
  /// </summary>
  public static Result InternalServerError(string message) =>
    InternalServerError(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
  /// Corresponds to HTTP status code 500 Internal Server Error.
  /// </summary>
  public static Result InternalServerError(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.InternalServerError);
  }

  /// <summary>
  /// Returns a result indicating the server does not support the functionality required to fulfill the request.
  /// Corresponds to HTTP status code 501 Not Implemented.
  /// </summary>
  public static Result NotImplemented(string message) =>
    NotImplemented(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server does not support the functionality required to fulfill the request.
  /// Corresponds to HTTP status code 501 Not Implemented.
  /// </summary>
  public static Result NotImplemented(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.NotImplemented);
  }

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
  /// Corresponds to HTTP status code 502 Bad Gateway.
  /// </summary>
  public static Result BadGateway(string message) =>
    BadGateway(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
  /// Corresponds to HTTP status code 502 Bad Gateway.
  /// </summary>
  public static Result BadGateway(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.BadGateway);
  }

  /// <summary>
  /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
  /// Corresponds to HTTP status code 503 Service Unavailable.
  /// </summary>
  public static Result ServiceUnavailable(string message) =>
    ServiceUnavailable(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
  /// Corresponds to HTTP status code 503 Service Unavailable.
  /// </summary>
  public static Result ServiceUnavailable(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.ServiceUnavailable);
  }

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
  /// Corresponds to HTTP status code 504 Gateway Timeout.
  /// </summary>
  public static Result GatewayTimeout(string message) =>
    GatewayTimeout(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
  /// Corresponds to HTTP status code 504 Gateway Timeout.
  /// </summary>
  public static Result GatewayTimeout(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.GatewayTimeout);
  }

  /// <summary>
  /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
  /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
  /// </summary>
  public static Result HttpVersionNotSupported(string message) =>
    HttpVersionNotSupported(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
  /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
  /// </summary>
  public static Result HttpVersionNotSupported(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.HttpVersionNotSupported);
  }

  /// <summary>
  /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
  /// Corresponds to HTTP status code 506 Variant Also Negotiates.
  /// </summary>
  public static Result VariantAlsoNegotiates(string message) =>
    VariantAlsoNegotiates(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
  /// Corresponds to HTTP status code 506 Variant Also Negotiates.
  /// </summary>
  public static Result VariantAlsoNegotiates(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.VariantAlsoNegotiates);
  }

  /// <summary>
  /// Returns a result indicating the server is unable to store the representation needed to complete the request.
  /// Corresponds to HTTP status code 507 Insufficient Storage.
  /// </summary>
  public static Result InsufficientStorage(string message) =>
    InsufficientStorage(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server is unable to store the representation needed to complete the request.
  /// Corresponds to HTTP status code 507 Insufficient Storage.
  /// </summary>
  public static Result InsufficientStorage(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.InsufficientStorage);
  }

  /// <summary>
  /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
  /// Corresponds to HTTP status code 508 Loop Detected.
  /// </summary>
  public static Result LoopDetected(string message) =>
    LoopDetected(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
  /// Corresponds to HTTP status code 508 Loop Detected.
  /// </summary>
  public static Result LoopDetected(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.LoopDetected);
  }

  /// <summary>
  /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
  /// Corresponds to HTTP status code 510 Not Extended.
  /// </summary>
  public static Result NotExtended(string message) =>
    NotExtended(new List<string> { message });

  /// <summary>
  /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
  /// Corresponds to HTTP status code 510 Not Extended.
  /// </summary>
  public static Result NotExtended(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.NotExtended);
  }

  /// <summary>
  /// Returns a result indicating the client needs to authenticate to gain network access.
  /// Corresponds to HTTP status code 511 Network Authentication Required.
  /// </summary>
  public static Result NetworkAuthenticationRequired(string message) =>
    NetworkAuthenticationRequired(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the client needs to authenticate to gain network access.
  /// Corresponds to HTTP status code 511 Network Authentication Required.
  /// </summary>
  public static Result NetworkAuthenticationRequired(List<string> messages) {
    return new Result(false, messages, HttpStatusCode.NetworkAuthenticationRequired);
  }
}

public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
  /// Corresponds to HTTP status code 500 Internal Server Error.
  /// </summary>
  public static Result<T> InternalServerError(T? value, string message) =>
    InternalServerError(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server encountered an unexpected condition that prevented it from fulfilling the request.
  /// Corresponds to HTTP status code 500 Internal Server Error.
  /// </summary>
  public static Result<T> InternalServerError(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.InternalServerError);
  }

  /// <summary>
  /// Returns a result indicating the server does not support the functionality required to fulfill the request.
  /// Corresponds to HTTP status code 501 Not Implemented.
  /// </summary>
  public static Result<T> NotImplemented(T? value, string message) =>
    NotImplemented(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server does not support the functionality required to fulfill the request.
  /// Corresponds to HTTP status code 501 Not Implemented.
  /// </summary>
  public static Result<T> NotImplemented(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.NotImplemented);
  }

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
  /// Corresponds to HTTP status code 502 Bad Gateway.
  /// </summary>
  public static Result<T> BadGateway(T? value, string message) =>
    BadGateway(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
  /// Corresponds to HTTP status code 502 Bad Gateway.
  /// </summary>
  public static Result<T> BadGateway(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.BadGateway);
  }

  /// <summary>
  /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
  /// Corresponds to HTTP status code 503 Service Unavailable.
  /// </summary>
  public static Result<T> ServiceUnavailable(T? value, string message) =>
    ServiceUnavailable(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server is currently unable to handle the request due to temporary overload or maintenance of the server.
  /// Corresponds to HTTP status code 503 Service Unavailable.
  /// </summary>
  public static Result<T> ServiceUnavailable(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.ServiceUnavailable);
  }

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
  /// Corresponds to HTTP status code 504 Gateway Timeout.
  /// </summary>
  public static Result<T> GatewayTimeout(T? value, string message) =>
    GatewayTimeout(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
  /// Corresponds to HTTP status code 504 Gateway Timeout.
  /// </summary>
  public static Result<T> GatewayTimeout(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.GatewayTimeout);
  }

  /// <summary>
  /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
  /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
  /// </summary>
  public static Result<T> HttpVersionNotSupported(T? value, string message) =>
    HttpVersionNotSupported(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server does not support the HTTP protocol version used in the request.
  /// Corresponds to HTTP status code 505 HTTP Version Not Supported.
  /// </summary>
  public static Result<T> HttpVersionNotSupported(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.HttpVersionNotSupported);
  }

  /// <summary>
  /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
  /// Corresponds to HTTP status code 506 Variant Also Negotiates.
  /// </summary>
  public static Result<T> VariantAlsoNegotiates(T? value, string message) =>
    VariantAlsoNegotiates(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
  /// Corresponds to HTTP status code 506 Variant Also Negotiates.
  /// </summary>
  public static Result<T> VariantAlsoNegotiates(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.VariantAlsoNegotiates);
  }

  /// <summary>
  /// Returns a result indicating the server is unable to store the representation needed to complete the request.
  /// Corresponds to HTTP status code 507 Insufficient Storage.
  /// </summary>
  public static Result<T> InsufficientStorage(T? value, string message) =>
    InsufficientStorage(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server is unable to store the representation needed to complete the request.
  /// Corresponds to HTTP status code 507 Insufficient Storage.
  /// </summary>
  public static Result<T> InsufficientStorage(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.InsufficientStorage);
  }

  /// <summary>
  /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
  /// Corresponds to HTTP status code 508 Loop Detected.
  /// </summary>
  public static Result<T> LoopDetected(T? value, string message) =>
    LoopDetected(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the server detected an infinite loop while processing a request with depth: infinity. Usually encountered in WebDAV scenarios.
  /// Corresponds to HTTP status code 508 Loop Detected.
  /// </summary>
  public static Result<T> LoopDetected(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.LoopDetected);
  }

  /// <summary>
  /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
  /// Corresponds to HTTP status code 510 Not Extended.
  /// </summary>
  public static Result<T> NotExtended(T? value, string message) =>
    NotExtended(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating further extensions to the request are required for the server to fulfill it.
  /// Corresponds to HTTP status code 510 Not Extended.
  /// </summary>
  public static Result<T> NotExtended(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.NotExtended);
  }

  /// <summary>
  /// Returns a result indicating the client needs to authenticate to gain network access.
  /// Corresponds to HTTP status code 511 Network Authentication Required.
  /// </summary>
  public static Result<T> NetworkAuthenticationRequired(T? value, string message) =>
    NetworkAuthenticationRequired(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the client needs to authenticate to gain network access.
  /// Corresponds to HTTP status code 511 Network Authentication Required.
  /// </summary>
  public static Result<T> NetworkAuthenticationRequired(T? value, List<string> messages) {
    return new Result<T>(value, false, messages, HttpStatusCode.NetworkAuthenticationRequired);
  }
}
