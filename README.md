# Anetta - Simple DI helper.

[![Build status](https://ci.appveyor.com/api/projects/status/gtme5qrorat8r37w?svg=true)](https://ci.appveyor.com/project/ocinbat/anetta)

Anetta is a tool for extending Microsoft.Extensions.DependencyInjection DI capabilities.

## Installation

```shell
PM> Install-Package Anetta
```

or

```shell
> dotnet add package Anetta
```

## Usage

In your ConfigureServices method return IServiceProvider built by Anetta.

```csharp
using Anetta.Extensions;

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddMvc();

    return services.BuildAnettaServiceProvider();
}
```

## Contributing

In lieu of a formal style guide, take care to maintain the existing coding style. Add unit tests for any new or changed functionality. Lint and test your code.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## References
- https://github.com/aspnet/DependencyInjection