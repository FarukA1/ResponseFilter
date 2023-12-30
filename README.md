# ResponseFilter

[![License](https://img.shields.io/github/license/FarukA1/ResponseFilter)](LICENSE)
[![Release](https://img.shields.io/github/v/release/FarukA1/ResponseFilter)](https://github.com/FarukA1/ResponseFilter/releases)
[![Issues](https://img.shields.io/github/issues/FarukA1/ResponseFilter)](https://github.com/FarukA1/ResponseFilter/issues)

## Overview

`ResponseFilter` is a lightweight C# library designed to simplify and customize the formatting of object responses in your applications. It provides a flexible way to filter and shape response data, allowing you to include or exclude specific fields based on your application's requirements.

## Key Features

- **Dynamic Response Formatting:** Dynamically shape and filter responses based on specified fields.
- **Exception Handling:** Provides detailed exception messages for easy debugging and error identification.
- **Integration with .NET Applications:** Seamlessly integrate into your .NET applications to enhance response customization.

## Getting Started

To get started, simply install the `ResponseFilter` package via NuGet Package Manager:

```bash
dotnet add package ResponseFilter
```

## Usage Example

### Code

```bash
    public async Task<IActionResult> Get()
    {
        var weatherForecast = new WeatherForecast()
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };

        var fields = new Fields
        {
            ResponseFields = "Date,TemperatureC"
        };

        var filteredResponse = Filter<WeatherForecast>.GetFilteredResponse(weatherForecast, fields);

        return Ok(filteredResponse);
    }
```

### Result

```bash
{
  "date": "2023-12-30",
  "temperaturec": 53
}
```

## Contribution

Contributions is welcomed. If you find a bug or have an enhancement in mind, feel free to open an issue or submit a pull request.


## License


