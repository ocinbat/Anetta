using System;
using System.Linq;
using System.Reflection;
using Anetta.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            foreach (Assembly assembly in AssemblyScanner.GetAssemblies())
            {
                foreach (TypeInfo ti in assembly.DefinedTypes)
                {
                    ConfigurationAttribute configurationAttribute = ti.GetCustomAttribute<ConfigurationAttribute>();

                    if (configurationAttribute != null)
                    {
                        string section = configurationAttribute.Section ?? ti.Name;

                        var method = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethods().FirstOrDefault(
                                x => x.Name.Equals("Configure", StringComparison.OrdinalIgnoreCase) &&
                                     x.IsGenericMethod && x.GetParameters().Length == 2)
                            ?.MakeGenericMethod(ti);

                        if (method == null)
                        {
                            throw new AmbiguousMatchException("There is no Configure extension method for service collection found. Are you using Microsoft.Extensions.Options.ConfigurationExtensions 2.2.0 or not?");
                        }

                        method.Invoke(null, new[] { serviceCollection, (object)configuration.GetSection(section) });
                    }
                }
            }

            return serviceCollection;
        }
    }
}