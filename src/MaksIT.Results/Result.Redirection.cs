using System.Net;


namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result MultipleChoices(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.MultipleChoices);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result MovedPermanently(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.MovedPermanently);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result Found(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.Found);
  }

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result SeeOther(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.SeeOther);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result NotModified(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.NotModified);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result UseProxy(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.UseProxy);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result TemporaryRedirect(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.TemporaryRedirect);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result PermanentRedirect(params string [] messages) {
    return new Result(true, [..messages], HttpStatusCode.PermanentRedirect);
  }
}

public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result<T> MultipleChoices(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.MultipleChoices);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result<T> MovedPermanently(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.MovedPermanently);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result<T> Found(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.Found);
  }

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result<T> SeeOther(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.SeeOther);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result<T> NotModified(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.NotModified);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result<T> UseProxy(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.UseProxy);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result<T> TemporaryRedirect(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.TemporaryRedirect);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result<T> PermanentRedirect(T? value, params string [] messages) {
    return new Result<T>(value, true, [..messages], HttpStatusCode.PermanentRedirect);
  }
}
