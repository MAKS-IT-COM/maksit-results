# MaksIT.Results

`MaksIT.Results` is a powerful library designed to streamline the creation and management of result objects in your ASP.NET Core applications. It provides a standardized way to handle method results and easily convert them to `IActionResult` for HTTP responses, ensuring consistent and clear API responses.

## Features

- **Standardized Result Handling**: Represent operation outcomes (success or failure) with appropriate HTTP status codes.
- **Seamless Conversion to `IActionResult`**: Convert result objects to HTTP responses (`IActionResult`) with detailed problem descriptions.
- **Flexible Result Types**: Supports both generic (`Result<T>`) and non-generic (`Result`) results for handling various scenarios.
- **Predefined Results for All Standard HTTP Status Codes**: Includes predefined static methods to create results for all standard HTTP status codes (e.g., 200 OK, 404 Not Found, 500 Internal Server Error, etc.).

## Installation

To install `MaksIT.Results`, use the NuGet Package Manager:

```bash
Install-Package MaksIT.Results
```

## Usage example

Below is an example demonstrating how to use `MaksIT.Results` in a typical ASP.NET Core application where a controller interacts with a service.

### Step 1: Define and Register the Service

Define a service that uses `MaksIT.Results` to return operation results, handling different result types with proper casting and conversion.

```csharp
public interface IVaultPersistanceService
{
    Result<Organization?> ReadOrganization(Guid organizationId);
    Task<Result> DeleteOrganizationAsync(Guid organizationId);
    // Additional method definitions...
}

public class VaultPersistanceService : IVaultPersistanceService
{
    // Inject dependencies as needed

    public Result<Organization?> ReadOrganization(Guid organizationId)
    {
        var organizationResult = _organizationDataProvider.GetById(organizationId);
        if (!organizationResult.IsSuccess || organizationResult.Value == null)
        {
            // Return a NotFound result when the organization isn't found
            return Result<Organization?>.NotFound("Organization not found.");
        }

        var organization = organizationResult.Value;
        var applicationDtos = new List<ApplicationDto>();

        foreach (var applicationId in organization.Applications)
        {
            var applicationResult = _applicationDataProvider.GetById(applicationId);
            if (!applicationResult.IsSuccess || applicationResult.Value == null)
            {
                // Transform the result from Result<Application?> to Result<Organization?>
                // Ensuring the return type matches the method signature (Result<Organization?>)
                return applicationResult.WithNewValue<Organization?>(_ => null);
            }

            var applicationDto = applicationResult.Value;
            applicationDtos.Add(applicationDto);
        }

        // Return the final result with all applications loaded
        return Result<Organization>.Ok(organization);
    }

    public async Task<Result> DeleteOrganizationAsync(Guid organizationId)
    {
        var organizationResult = await _organizationDataProvider.GetByIdAsync(organizationId);

        if (!organizationResult.IsSuccess || organizationResult.Value == null)
        {
            // Convert Result<Organization?> to a non-generic Result
            // The cast to (Result) allows for standardized response type
            return (Result)organizationResult;
        }

        // Proceed with the deletion if the organization is found
        var deleteResult = await _organizationDataProvider.DeleteByIdAsync(organizationId);

        // Return the result of the delete operation directly
        return deleteResult;
    }
}
```

**Key Points to Note:**

1. **Handling Different Result Types:**
   - The `ReadOrganization` method demonstrates handling a `Result<Organization?>` and transforming other types as needed using `WithNewValue<T>`. This ensures the method always returns the correct type.
   
2. **Casting from `Result<T>` to `Result`:**
   - In `DeleteOrganizationAsync`, we cast `Result<Organization?>` to `Result` using `(Result)organizationResult`. This cast standardizes the result type, making it suitable for scenarios where only success or failure matters.

Ensure this service is registered in your dependency injection container:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IVaultPersistanceService, VaultPersistanceService>();
    // Other service registrations...
}
```

### Step 2: Use the Service in the Controller

Inject the service into your controller and utilize `MaksIT.Results` to handle results efficiently:

```csharp
using Microsoft.AspNetCore.Mvc;
using MaksIT.Results;

public class OrganizationController : ControllerBase
{
    private readonly IVaultPersistanceService _vaultPersistanceService;

    public OrganizationController(IVaultPersistanceService vaultPersistanceService)
    {
        _vaultPersistanceService = vaultPersistanceService;
    }

    [HttpGet("{organizationId}")]
    public IActionResult GetOrganization(Guid organizationId)
    {
        var result = _vaultPersistanceService.ReadOrganization(organizationId);

        // Convert the Result to IActionResult using ToActionResult()
        return result.ToActionResult();
    }

    [HttpDelete("{organizationId}")]
    public async Task<IActionResult> DeleteOrganization(Guid organizationId)
    {
        var result = await _vaultPersistanceService.DeleteOrganizationAsync(organizationId);

        // Convert the Result to IActionResult using ToActionResult()
        return result.ToActionResult();
    }

    // Additional actions...
}
```

### Transforming Results

You can also transform the result within the controller or service to adjust the output type as needed:

```csharp
public IActionResult TransformResultExample()
{
    var result = _vaultPersistanceService.ReadOrganization(Guid.NewGuid());

    // Transform the result to a different type if needed
    var transformedResult = result.WithNewValue<string>(org => (org?.Name ?? "").ToTitle());

    return transformedResult.ToActionResult();
}
```

### Predefined Results for All Standard HTTP Status Codes

`MaksIT.Results` provides methods to easily create results for all standard HTTP status codes, simplifying the handling of responses:

```csharp
return Result.Ok<string?>("Success").ToActionResult();                // 200 OK
return Result.NotFound<string?>("Resource not found").ToActionResult(); // 404 Not Found
return Result.InternalServerError<string?>("An unexpected error occurred").ToActionResult(); // 500 Internal Server Error
```

### Conclusion

`MaksIT.Results` is a powerful tool for simplifying the handling of operation results in ASP.NET Core applications. It provides a robust framework for standardized result handling, seamless conversion to `IActionResult`, and flexible result types to handle various scenarios. By adopting this library, developers can create more maintainable and readable code, ensuring consistent and clear HTTP responses.

## Contribution

Contributions to this project are welcome! Please fork the repository and submit a pull request with your changes. If you encounter any issues or have feature requests, feel free to open an issue on GitHub.

## Contact

If you have any questions or need further assistance, feel free to reach out:

- **Email**: [maksym.sadovnychyy@gmail.com](mailto:maksym.sadovnychyy@gmail.com)
- **Reddit**: [MaksIT.Results: Streamline Your ASP.NET Core API Response Handling](https://www.reddit.com/r/MaksIT/comments/1f89ifn/maksitresults_streamline_your_aspnet_core_api/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button)

## License

This project is licensed under the MIT License. See the full license text below.

---

### MIT License

```
MIT License

Copyright (c) 2024 Maksym Sadovnychyy (MAKS-IT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Contact

For any questions or inquiries, please reach out via GitHub or [email](mailto:maksym.sadovnychyy@gmail.com).