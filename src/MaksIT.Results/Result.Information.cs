using System.Net;


namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating that the initial part of a request has been received and the client should continue with the request.
  /// Corresponds to HTTP status code 100 Continue.
  /// </summary>
  public static Result Continue(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.Continue);
  }

  /// <summary>
  /// Returns a result indicating that the server is switching to a different protocol as requested by the client.
  /// Corresponds to HTTP status code 101 Switching Protocols.
  /// </summary>
  public static Result SwitchingProtocols(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.SwitchingProtocols);
  }

  /// <summary>
  /// Returns a result indicating that the server has received and is processing the request, but no response is available yet.
  /// Corresponds to HTTP status code 102 Processing.
  /// </summary>
  public static Result Processing(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.Processing);
  }

  /// <summary>
  /// Returns a result indicating that the server is sending information about early hints that may be used by the client to begin preloading resources while the server prepares a final response.
  /// Corresponds to HTTP status code 103 Early Hints.
  /// </summary>
  public static Result EarlyHints(params string [] messages) {
    return new Result(true, [..messages], (HttpStatusCode)103); // Early Hints is not defined in HttpStatusCode enum, 103 is the official code
  }
}

public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating that the initial part of a request has been received and the client should continue with the request.
  /// Corresponds to HTTP status code 100 Continue.
  /// </summary>
  public static Result<T> Continue(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.Continue);
  }

  /// <summary>
  /// Returns a result indicating that the server is switching to a different protocol as requested by the client.
  /// Corresponds to HTTP status code 101 Switching Protocols.
  /// </summary>
  public static Result<T> SwitchingProtocols(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.SwitchingProtocols);
  }

  /// <summary>
  /// Returns a result indicating that the server has received and is processing the request, but no response is available yet.
  /// Corresponds to HTTP status code 102 Processing.
  /// </summary>
  public static Result<T> Processing(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.Processing);
  }

  /// <summary>
  /// Returns a result indicating that the server is sending information about early hints that may be used by the client to begin preloading resources while the server prepares a final response.
  /// Corresponds to HTTP status code 103 Early Hints.
  /// </summary>
  public static Result<T> EarlyHints(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], (HttpStatusCode)103); // Early Hints is not defined in HttpStatusCode enum, 103 is the official code
  }
}

