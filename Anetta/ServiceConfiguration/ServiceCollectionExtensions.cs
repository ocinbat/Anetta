using System;
using Anetta.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceConfigurators(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            foreach (Type type in AssemblyScanner.GetAllTypesOfInterface<IServiceConfigurator>())
            {
                IServiceConfigurator configurator = (IServiceConfigurator)Activator.CreateInstance(type);
                configurator.Configure(serviceCollection, configuration);
            }

            return serviceCollection;
        }
    }
}