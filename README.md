# MaksIT.Results

[![NuGet](https://img.shields.io/nuget/v/MaksIT.Results)](https://www.nuget.org/packages/MaksIT.Results)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue)](LICENSE.md)

![Line Coverage](https://img.shields.io/badge/Line%20Coverage-44.8%25-yellowgreen)
![Branch Coverage](https://img.shields.io/badge/Branch%20Coverage-54.7%25-yellowgreen)
![Method Coverage](https://img.shields.io/badge/Method%20Coverage-20.5%25-yellow)

HTTP-aware `Result` / `Result<T>` for .NET - model success and failure once, then map to ASP.NET Core `IActionResult` or parse the same wire shape from an `HttpResponseMessage`.

**Target:** .NET 10 (`net10.0`)

## Why

Most Result libraries give you success/failure (and sometimes a typed error), then leave HTTP as a second mapping in the controller: domain `NotFound` -> 404, `Conflict` -> 409, and so on. That remapping is where APIs drift.

**MaksIT.Results uses HTTP status codes as the outcome vocabulary.** One type carries success/failure, messages, and a shared classification (`NotFound`, `Conflict`, `TooManyRequests`, ...). The service already decided the status; the controller calls `ToActionResult()`.

That is not "leaking HTTP into the domain." Status codes are a mature taxonomy of outcomes that apply beyond the wire - including desktop, workers, and CLI:

| Status idea | Same idea outside the browser |
|-------------|-------------------------------|
| 404 | entity / file / setting not found |
| 409 | concurrent edit / state conflict |
| 401 / 403 | not signed in / not allowed |
| 422 | validation failed |
| 429 | rate limit / throttle |
| 503 | dependency down / retry later |
| 202 | accepted, still processing |

### Compared to typical Result libs

| | Typical Result lib | MaksIT.Results |
|--|--------------------|----------------|
| Outcome model | Success / Failure (+ custom errors) | Success / Failure + `HttpStatusCode` |
| Controller mapping | Invent codes, map again to HTTP | Status already set; `ToActionResult()` |
| Failure body | Often ad hoc | RFC 7807 `ProblemDetails` |
| Client parse | Rare / out of scope | `FromHttpResponseAsync` / `FromActionResult` |
| Status factories | Usually `Ok` / `Fail` (+ a few) | Named 1xx-5xx factories |

**Bottom line:** most Result packages stop at "return from a method." This one is a service <-> API <-> client result model - one contract from domain to wire and back.

## Install

```bash
dotnet add package MaksIT.Results
```

```powershell
Install-Package MaksIT.Results
```

## Quick start

### Create results

```csharp
using MaksIT.Results;

Result ok = Result.Ok("Operation completed");
Result failed = Result.BadRequest("Validation failed");

Result<int> answer = Result<int>.Ok(42, "Answer generated");
Result<UserDto?> missing = Result<UserDto?>.NotFound(null, "Entity not found");
```

### Map between result shapes

```csharp
Result<int> source = Result<int>.Ok(42, "Value loaded");

Result<string?> asString = source.ToResultOfType(v => v?.ToString());
Result nonGeneric = source.ToResult();
```

### Controllers

```csharp
using MaksIT.Results;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public sealed class UsersController : ControllerBase {
  [HttpGet("{id:guid}")]
  public IActionResult Get(Guid id) {
    Result<UserDto?> result = id == Guid.Empty
      ? Result<UserDto?>.BadRequest(null, "Invalid id")
      : Result<UserDto?>.Ok(new UserDto(id, "maks"), "User loaded");

    return result.ToActionResult();
  }
}

public sealed record UserDto(Guid Id, string Name);
```

### HTTP clients

Parse responses produced in the same contract (JSON body, redirect `Location`, or `problem+json`):

```csharp
using HttpResponseMessage response = await http.GetAsync(url);

Result bare = await Result.FromHttpResponseAsync(response);
Result<UserDto> typed = await Result<UserDto>.FromHttpResponseAsync(response);
```

Or reverse an `IActionResult` you already built:

```csharp
IActionResult action = result.ToActionResult();

Result parsed = Result.FromActionResult(action);
Result<UserDto> typed = Result<UserDto>.FromActionResult(action);
```

## `ToActionResult()` contract

| Outcome | Response |
|--------|----------|
| `Result` success | Status code only |
| `Result<T>` success, non-null `Value` | JSON body + status |
| `Result<T>` success, redirect status (300-303, 307-308) and non-empty string `Value` | Redirect; status preserved on `RedirectResult` |
| Any failure | `ProblemDetails`: `status`, `title` = `"An error occurred"`, `detail` = joined `Messages`, content type `application/problem+json` |

## `FromActionResult` / `FromHttpResponseAsync`

| Wire shape | Result |
|------------|--------|
| Success, no body | Success `Result` / empty `Result<T>` |
| Success JSON body | Deserialized into `Result<T>.Value` |
| Redirect + `Location` | `Result<string>` with that URL and redirect status |
| `application/problem+json` (or JSON with `detail` / `title`) | Failure; messages from problem details |

## JSON options

`ObjectResult` uses the app's `JsonSerializerOptions` when configured via `AddJsonOptions`:

```csharp
builder.Services.AddControllers()
  .AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });
```

If none are registered, responses fall back to camel case.

## Status code factories

| Family | Examples |
|--------|----------|
| Informational | `Continue`, `SwitchingProtocols`, `Processing`, ... |
| Success | `Ok`, `Created`, `NoContent`, ... |
| Redirection | `Found`, `MovedPermanently`, `PermanentRedirect`, ... |
| Client error | `BadRequest`, `NotFound`, `Conflict`, `TooManyRequests`, ... |
| Server error | `InternalServerError`, `ServiceUnavailable`, ... |

Use `Result.*` or `Result<T>.*` (e.g. `Result<MyDto>.Ok(value, "message")`).

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Changelog: [CHANGELOG.md](CHANGELOG.md).

## License

[MIT](LICENSE.md) - Maksym Sadovnychyy (MAKS-IT)

**Contact:** [maksym.sadovnychyy@gmail.com](mailto:maksym.sadovnychyy@gmail.com)
