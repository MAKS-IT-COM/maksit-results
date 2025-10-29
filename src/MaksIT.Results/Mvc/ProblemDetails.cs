using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaksIT.Results.Mvc;

public class ProblemDetails {
  public int? Status { get; set; }
  public string? Title { get; set; }
  public string? Detail { get; set; }
  public string? Instance { get; set; }
  public IDictionary<string, object?> Extensions { get; } = new Dictionary<string, object?>();
}