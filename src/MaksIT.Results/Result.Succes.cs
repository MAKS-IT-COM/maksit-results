using System.Net;

namespace MaksIT.Results {

  public partial class Result {

    /// <summary>
    /// Returns a result indicating the request was successful and the server returned the requested data.
    /// Corresponds to HTTP status code 200 OK.
    /// </summary>
    public static Result Ok(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.OK);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and a new resource was created.
    /// Corresponds to HTTP status code 201 Created.
    /// </summary>
    public static Result Created(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.Created);
    }

    /// <summary>
    /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
    /// Corresponds to HTTP status code 202 Accepted.
    /// </summary>
    public static Result Accepted(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.Accepted);
    }

    /// <summary>
    /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
    /// Corresponds to HTTP status code 203 Non-Authoritative Information.
    /// </summary>
    public static Result NonAuthoritativeInformation(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.NonAuthoritativeInformation);
    }

    /// <summary>
    /// Returns a result indicating the request was successful but there is no content to send in the response.
    /// Corresponds to HTTP status code 204 No Content.
    /// </summary>
    public static Result NoContent(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.NoContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
    /// Corresponds to HTTP status code 205 Reset Content.
    /// </summary>
    public static Result ResetContent(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.ResetContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
    /// Corresponds to HTTP status code 206 Partial Content.
    /// </summary>
    public static Result PartialContent(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.PartialContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
    /// Corresponds to HTTP status code 207 Multi-Status.
    /// </summary>
    public static Result MultiStatus(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.MultiStatus);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
    /// Corresponds to HTTP status code 208 Already Reported.
    /// </summary>
    public static Result AlreadyReported(params string[] messages) {
      return new Result(true, new List<string>(messages), HttpStatusCode.AlreadyReported);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
    /// Corresponds to HTTP status code 226 IM Used.
    /// </summary>
    public static Result IMUsed(params string[] messages) {
      return new Result(true, new List<string>(messages), (HttpStatusCode)226); // 226 is the official status code for IM Used
    }
  }
  public partial class Result<T> : Result {

    /// <summary>
    /// Returns a result indicating the request was successful and the server returned the requested data.
    /// Corresponds to HTTP status code 200 OK.
    /// </summary>
    public static Result<T> Ok(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.OK);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and a new resource was created.
    /// Corresponds to HTTP status code 201 Created.
    /// </summary>
    public static Result<T> Created(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.Created);
    }

    /// <summary>
    /// Returns a result indicating the request has been accepted for processing, but the processing is not yet complete.
    /// Corresponds to HTTP status code 202 Accepted.
    /// </summary>
    public static Result<T> Accepted(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.Accepted);
    }

    /// <summary>
    /// Returns a result indicating the request was successful but the response contains metadata from a source other than the origin server.
    /// Corresponds to HTTP status code 203 Non-Authoritative Information.
    /// </summary>
    public static Result<T> NonAuthoritativeInformation(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.NonAuthoritativeInformation);
    }

    /// <summary>
    /// Returns a result indicating the request was successful but there is no content to send in the response.
    /// Corresponds to HTTP status code 204 No Content.
    /// </summary>
    public static Result<T> NoContent(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.NoContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful, but the user-agent should reset the document view that caused the request.
    /// Corresponds to HTTP status code 205 Reset Content.
    /// </summary>
    public static Result<T> ResetContent(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.ResetContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the server is delivering only part of the resource due to a range header sent by the client.
    /// Corresponds to HTTP status code 206 Partial Content.
    /// </summary>
    public static Result<T> PartialContent(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.PartialContent);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the response contains multiple status codes, typically used for WebDAV.
    /// Corresponds to HTTP status code 207 Multi-Status.
    /// </summary>
    public static Result<T> MultiStatus(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.MultiStatus);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the information has already been reported in a previous response.
    /// Corresponds to HTTP status code 208 Already Reported.
    /// </summary>
    public static Result<T> AlreadyReported(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), HttpStatusCode.AlreadyReported);
    }

    /// <summary>
    /// Returns a result indicating the request was successful and the server fulfilled the request for the resource using the delta encoding method.
    /// Corresponds to HTTP status code 226 IM Used.
    /// </summary>
    public static Result<T> IMUsed(T? value, params string[] messages) {
      return new Result<T>(value, true, new List<string>(messages), (HttpStatusCode)226); // 226 is the official status code for IM Used
    }
  }
}
