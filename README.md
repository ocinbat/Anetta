# Anetta - Simple DI helper.

[![Build status](https://ci.appveyor.com/api/projects/status/gtme5qrorat8r37w?svg=true)](https://ci.appveyor.com/project/ocinbat/anetta)

Anetta is a tool for extending Microsoft.Extensions.DependencyInjection DI capabilities with annotations.

## Installation

```shell
PM> Install-Package Anetta
```

or

```shell
> dotnet add package Anetta
```

## Annotations Example

In your ```ConfigureServices``` method use ```AddAnnotations``` method provided by Anetta.Extensions namespace.

```csharp
using Anetta.Extensions;

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddAnnotations();

    /// other service registrations
}
```

Now you can mark your classes with 3 different lifetime attributes: ```Singleton, Scoped, Transient```.

```csharp
using Anetta.Attributes;

[Singleton]
public class SomeClass
{
    /// other class behaviour
}
```

## ServiceConfigurator Example

You can extract logic from ```ConfigureServices``` to seperate classes by using ```IServiceConfigurator``` interface.

Create a class that implements ```IServiceConfigurator``` interface.

```csharp
using Anetta.ServiceConfiguration;

public class SampleServiceWithConfiguratorConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SampleServiceWithConfigurator>();
    }
}
```

and in your ```ConfigureServices``` method use ```AddServiceConfigurators``` method provided by ```Anetta.ServiceConfiguration``` namespace.

```csharp
using Anetta.ServiceConfiguration;

public IConfiguration Configuration { get; }

public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddServiceConfigurators(Configuration);

    /// other service registrations
}
```

## Contributing

In lieu of a formal style guide, take care to maintain the existing coding style. Add unit tests for any new or changed functionality. Lint and test your code.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## References
- https://github.com/aspnet/DependencyInjection