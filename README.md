# MaksIT.Results

![Line Coverage](assets/badges/coverage-lines.svg) ![Branch Coverage](assets/badges/coverage-branches.svg) ![Method Coverage](assets/badges/coverage-methods.svg)

`MaksIT.Results` is a .NET library for modeling operation outcomes as HTTP-aware result objects and converting them to `IActionResult` in ASP.NET Core.

## Features

- `Result` and `Result<T>` models with status code, success flag, and messages.
- Static factory methods for common and extended HTTP status codes (1xx, 2xx, 3xx, 4xx, 5xx).
- Built-in conversion to `IActionResult` via `ToActionResult()`.
- RFC 7807-style error payloads for failures (`application/problem+json`).
- Camel-case JSON serialization for response bodies; respects app-configured `JsonSerializerOptions` (e.g. `AddJsonOptions` with `DefaultIgnoreCondition.WhenWritingNull`).

## Installation

Package Manager:

```bash
Install-Package MaksIT.Results
```

`dotnet` CLI:

```bash
dotnet add package MaksIT.Results
```

## Target Framework

- `.NET 10` (`net10.0`)

## Quick Start

### Create results

```csharp
using MaksIT.Results;

Result ok = Result.Ok("Operation completed");
Result failed = Result.BadRequest("Validation failed");

Result<int> okWithValue = Result<int>.Ok(42, "Answer generated");
Result<string?> notFound = Result<string?>.NotFound(null, "Entity not found");
```

### Convert between result types

```csharp
using MaksIT.Results;

Result<int> source = Result<int>.Ok(42, "Value loaded");

// Result<T> -> Result<U>
Result<string?> mapped = source.ToResultOfType(v => v?.ToString());

// Result<T> -> Result
Result nonGeneric = source.ToResult();
```

### Use in an ASP.NET Core controller

```csharp
using MaksIT.Results;
using Microsoft.AspNetCore.Mvc;

public sealed class UsersController : ControllerBase {
  [HttpGet("{id:guid}")]
  public IActionResult GetUser(Guid id) {
    Result<UserDto?> result = id == Guid.Empty
      ? Result<UserDto?>.BadRequest(null, "Invalid id")
      : Result<UserDto?>.Ok(new UserDto(id, "maks"), "User loaded");

    return result.ToActionResult();
  }
}

public sealed record UserDto(Guid Id, string Name);
```

## `ToActionResult()` Behavior

- `Result` success: returns status-code-only response.
- `Result<T>` success with non-null `Value`: returns JSON body + status code.
- Any failure: returns RFC 7807-style `ProblemDetails` JSON with:
  - `status` = result status code
  - `title` = `"An error occurred"`
  - `detail` = joined `Messages`
  - content type `application/problem+json`

## JSON options

`ObjectResult` uses the same `JsonSerializerOptions` as your app when you configure them with `AddJsonOptions`:

```csharp
builder.Services.AddControllers()
  .AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });
```

If no options are registered, a default (camel-case) serializer is used.

## Status Code Factories

- Informational: `Result.Continue(...)`, `Result.SwitchingProtocols(...)`, `Result.Processing(...)`, etc.
- Success: `Result.Ok(...)`, `Result.Created(...)`, `Result.NoContent(...)`, etc.
- Redirection: `Result.Found(...)`, `Result.PermanentRedirect(...)`, etc.
- Client error: `Result.BadRequest(...)`, `Result.NotFound(...)`, `Result.TooManyRequests(...)`, etc.
- Server error: `Result.InternalServerError(...)`, `Result.ServiceUnavailable(...)`, etc.

Generic equivalents are available via `Result<T>`, for example `Result<MyDto>.Ok(value, "message")`.

## Contributing

See `CONTRIBUTING.md`.

## Contact

If you have any questions or need further assistance, feel free to reach out:

- **Email**: [maksym.sadovnychyy@gmail.com](mailto:maksym.sadovnychyy@gmail.com)
- **Reddit**: [MaksIT.Results: Streamline Your ASP.NET Core API Response Handling](https://www.reddit.com/r/MaksIT/comments/1f89ifn/maksitresults_streamline_your_aspnet_core_api/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button)

## License

See `LICENSE.md`.