using System.Net;


namespace MaksIT.Results;

public partial class Result {

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result MultipleChoices(string message) =>
    MultipleChoices(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result MultipleChoices(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.MultipleChoices);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result MovedPermanently(string message) =>
    MovedPermanently(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result MovedPermanently(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.MovedPermanently);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result Found(string message) =>
    Found(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result Found(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.Found);
  }

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result SeeOther(string message) =>
    SeeOther(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result SeeOther(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.SeeOther);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result NotModified(string message) =>
    NotModified(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result NotModified(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.NotModified);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result UseProxy(string message) =>
    UseProxy(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result UseProxy(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.UseProxy);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result TemporaryRedirect(string message) =>
    TemporaryRedirect(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result TemporaryRedirect(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.TemporaryRedirect);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result PermanentRedirect(string message) =>
    PermanentRedirect(new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result PermanentRedirect(List<string> messages) {
    return new Result(true, messages, HttpStatusCode.PermanentRedirect);
  }
}

public partial class Result<T> : Result {

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result<T> MultipleChoices(T? value, string message) =>
    MultipleChoices(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the request has multiple options, and the user or user-agent should select one of them.
  /// Corresponds to HTTP status code 300 Multiple Choices.
  /// </summary>
  public static Result<T> MultipleChoices(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.MultipleChoices);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result<T> MovedPermanently(T? value, string message) =>
    MovedPermanently(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI.
  /// Corresponds to HTTP status code 301 Moved Permanently.
  /// </summary>
  public static Result<T> MovedPermanently(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.MovedPermanently);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result<T> Found(T? value, string message) =>
    Found(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI.
  /// Corresponds to HTTP status code 302 Found.
  /// </summary>
  public static Result<T> Found(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.Found);
  }

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result<T> SeeOther(T? value, string message) =>
    SeeOther(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the response to the request can be found under another URI using the GET method.
  /// Corresponds to HTTP status code 303 See Other.
  /// </summary>
  public static Result<T> SeeOther(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.SeeOther);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result<T> NotModified(T? value, string message) =>
    NotModified(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has not been modified since the last request.
  /// Corresponds to HTTP status code 304 Not Modified.
  /// </summary>
  public static Result<T> NotModified(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.NotModified);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result<T> UseProxy(T? value, string message) =>
    UseProxy(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource must be accessed through the proxy given by the location field.
  /// Corresponds to HTTP status code 305 Use Proxy.
  /// </summary>
  public static Result<T> UseProxy(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.UseProxy);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result<T> TemporaryRedirect(T? value, string message) =>
    TemporaryRedirect(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource resides temporarily under a different URI, but future requests should still use the original URI.
  /// Corresponds to HTTP status code 307 Temporary Redirect.
  /// </summary>
  public static Result<T> TemporaryRedirect(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.TemporaryRedirect);
  }

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result<T> PermanentRedirect(T? value, string message) =>
    PermanentRedirect(value, new List<string> { message });

  /// <summary>
  /// Returns a result indicating that the requested resource has been permanently moved to a new URI, and future references should use the new URI.
  /// Corresponds to HTTP status code 308 Permanent Redirect.
  /// </summary>
  public static Result<T> PermanentRedirect(T? value, List<string> messages) {
    return new Result<T>(value, true, messages, HttpStatusCode.PermanentRedirect);
  }
}
