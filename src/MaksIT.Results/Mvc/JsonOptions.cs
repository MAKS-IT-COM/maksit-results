
using System.Text.Json;

namespace MaksIT.Results.Mvc;

//
// Summary:
//     Options to configure Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter
//     and Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.
public class JsonOptions {
  public JsonOptions() {
    JsonSerializerOptions = new JsonSerializerOptions();
  }

  //
  // Summary:
  //     Gets or sets a flag to determine whether error messages from JSON deserialization
  //     by the Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter will
  //     be added to the Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary. If
  //     false, a generic error message will be used instead.
  //
  // Value:
  //     The default value is true.
  //
  // Remarks:
  //     Error messages in the Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary
  //     are often communicated to clients, either in HTML or using Microsoft.AspNetCore.Mvc.BadRequestObjectResult.
  //     In effect, this setting controls whether clients can receive detailed error messages
  //     about submitted JSON data.
  public bool AllowInputFormatterExceptionMessages { get; set; }
  //
  // Summary:
  //     Gets the System.Text.Json.JsonSerializerOptions used by Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter
  //     and Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.
  public JsonSerializerOptions JsonSerializerOptions { get; }
}