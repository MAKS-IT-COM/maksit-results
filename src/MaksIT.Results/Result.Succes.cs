using System.Net;


namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating the request was successful and the server returned the requested data.
  /// Corresponds to HTTP status code 200 OK.
  /// </summary>
  public static Result Ok(string message) =>
    Ok(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server returned the requested data.
  /// Corresponds to HTTP status code 200 OK.
  /// </summary>
  public static Result Ok(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.OK);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and a new resource was created.
  /// Corresponds to HTTP status code 201 Created.
  /// </summary>
  public static Result Created(string message) =>
    Created(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and a new resource was created.
  /// Corresponds to HTTP status code 201 Created.
  /// </summary>
  public static Result Created(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.Created);
  }

  /// <summary>
  /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
  /// Corresponds to HTTP status code 202 Accepted.
  /// </summary>
  public static Result Accepted(string message) =>
    Accepted(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
  /// Corresponds to HTTP status code 202 Accepted.
  /// </summary>
  public static Result Accepted(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.Accepted);
  }

  /// <summary>
  /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
  /// Corresponds to HTTP status code 203 Non-Authoritative Information.
  /// </summary>
  public static Result NonAuthoritativeInformation(string message) =>
    NonAuthoritativeInformation(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
  /// Corresponds to HTTP status code 203 Non-Authoritative Information.
  /// </summary>
  public static Result NonAuthoritativeInformation(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.NonAuthoritativeInformation);
  }

  /// <summary>
  /// Returns a result indicating the request was successful but there is no content to send in the response.
  /// Corresponds to HTTP status code 204 No Content.
  /// </summary>
  public static Result NoContent(string message) =>
    ResetContent(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful but there is no content to send in the response.
  /// Corresponds to HTTP status code 204 No Content.
  /// </summary>
  public static Result NoContent(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.NoContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
  /// Corresponds to HTTP status code 205 Reset Content.
  /// </summary>
  public static Result ResetContent(string message) =>
    ResetContent(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
  /// Corresponds to HTTP status code 205 Reset Content.
  /// </summary>
  public static Result ResetContent(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.ResetContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
  /// Corresponds to HTTP status code 206 Partial Content.
  /// </summary>
  public static Result PartialContent(string message) =>
    PartialContent(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
  /// Corresponds to HTTP status code 206 Partial Content.
  /// </summary>
  public static Result PartialContent(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.PartialContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
  /// Corresponds to HTTP status code 207 Multi-Status.
  /// </summary>
  public static Result MultiStatus(string message) =>
    MultiStatus(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
  /// Corresponds to HTTP status code 207 Multi-Status.
  /// </summary>
  public static Result MultiStatus(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.MultiStatus);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
  /// Corresponds to HTTP status code 208 Already Reported.
  /// </summary>
  public static Result AlreadyReported(string message) =>
    AlreadyReported(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
  /// Corresponds to HTTP status code 208 Already Reported.
  /// </summary>
  public static Result AlreadyReported(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.AlreadyReported);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
  /// Corresponds to HTTP status code 226 IM Used.
  /// </summary>
  public static Result IMUsed(string message) =>
    IMUsed(new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
  /// Corresponds to HTTP status code 226 IM Used.
  /// </summary>
  public static Result IMUsed(List<string> messages) {
    return new Result(true, messages, (HttpStatusCode)226); // 226 is the official status code for IM Used
  }
}
public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating the request was successful and the server returned the requested data.
  /// Corresponds to HTTP status code 200 OK.
  /// </summary>
  public static Result<T> Ok(T? value, string message) =>
    Ok(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server returned the requested data.
  /// Corresponds to HTTP status code 200 OK.
  /// </summary>
  public static Result<T> Ok(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.OK);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and a new resource was created.
  /// Corresponds to HTTP status code 201 Created.
  /// </summary>
  public static Result<T> Created(T? value, string message) =>
    Created(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and a new resource was created.
  /// Corresponds to HTTP status code 201 Created.
  /// </summary>
  public static Result<T> Created(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.Created);
  }

  /// <summary>
  /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
  /// Corresponds to HTTP status code 202 Accepted.
  /// </summary>
  public static Result<T> Accepted(T? value, string message) =>
    Accepted(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
  /// Corresponds to HTTP status code 202 Accepted.
  /// </summary>
  public static Result<T> Accepted(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.Accepted);
  }

  /// <summary>
  /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
  /// Corresponds to HTTP status code 203 Non-Authoritative Information.
  /// </summary>
  public static Result<T> NonAuthoritativeInformation(T? value, string message) =>
    NonAuthoritativeInformation(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
  /// Corresponds to HTTP status code 203 Non-Authoritative Information.
  /// </summary>
  public static Result<T> NonAuthoritativeInformation(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.NonAuthoritativeInformation);
  }

  /// <summary>
  /// Returns a result indicating the request was successful but there is no content to send in the response.
  /// Corresponds to HTTP status code 204 No Content.
  /// </summary>
  public static Result<T> NoContent(T? value, string message) =>
    NoContent(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful but there is no content to send in the response.
  /// Corresponds to HTTP status code 204 No Content.
  /// </summary>
  public static Result<T> NoContent(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.NoContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
  /// Corresponds to HTTP status code 205 Reset Content.
  /// </summary>
  public static Result<T> ResetContent(T? value, string message) =>
    ResetContent(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
  /// Corresponds to HTTP status code 205 Reset Content.
  /// </summary>
  public static Result<T> ResetContent(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.ResetContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
  /// Corresponds to HTTP status code 206 Partial Content.
  /// </summary>
  public static Result<T> PartialContent(T? value, string message) =>
    PartialContent(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
  /// Corresponds to HTTP status code 206 Partial Content.
  /// </summary>
  public static Result<T> PartialContent(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.PartialContent);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
  /// Corresponds to HTTP status code 207 Multi-Status.
  /// </summary>
  public static Result<T> MultiStatus(T? value, string message) =>
    MultiStatus(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
  /// Corresponds to HTTP status code 207 Multi-Status.
  /// </summary>
  public static Result<T> MultiStatus(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.MultiStatus);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
  /// Corresponds to HTTP status code 208 Already Reported.
  /// </summary>
  public static Result<T> AlreadyReported(T? value, string message) =>
    AlreadyReported(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
  /// Corresponds to HTTP status code 208 Already Reported.
  /// </summary>
  public static Result<T> AlreadyReported(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.AlreadyReported);
  }

  /// <summary>
  /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
  /// Corresponds to HTTP status code 226 IM Used.
  /// </summary>
  public static Result<T> IMUsed(T? value, string message) =>
    IMUsed(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
  /// Corresponds to HTTP status code 226 IM Used.
  /// </summary>
  public static Result<T> IMUsed(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, (HttpStatusCode)226); // 226 is the official status code for IM Used
  }
}
