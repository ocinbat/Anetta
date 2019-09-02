using System;
using System.Reflection;
using Anetta.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAnnotations(this IServiceCollection serviceCollection)
        {
            ScanAndRegisterAllAssemlies(serviceCollection);
            return serviceCollection;
        }

        private static void ScanAndRegisterAllAssemlies(IServiceCollection serviceCollection)
        {
            // TODO: Implement a smarter assembly scanner.
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ScanAndRegisterAssemblyByAttributes(entryAssembly, serviceCollection);

            foreach (AssemblyName assemblyName in entryAssembly.GetReferencedAssemblies())
            {
                Assembly assembly = Assembly.Load(assemblyName);
                ScanAndRegisterAssemblyByAttributes(assembly, serviceCollection);
            }
        }

        private static void ScanAndRegisterAssemblyByAttributes(Assembly assembly, IServiceCollection serviceCollection)
        {
            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (Attribute.GetCustomAttribute(ti, typeof(TransientAttribute)) != null)
                {
                    serviceCollection.AddTransient((Type)ti);

                    foreach (Type implementedInterface in ti.ImplementedInterfaces)
                    {
                        serviceCollection.AddTransient(implementedInterface, ti);
                    }
                }

                if (Attribute.GetCustomAttribute(ti, typeof(ScopedAttribute)) != null)
                {
                    serviceCollection.AddScoped((Type)ti);

                    foreach (Type implementedInterface in ti.ImplementedInterfaces)
                    {
                        serviceCollection.AddScoped(implementedInterface, ti);
                    }
                }

                if (Attribute.GetCustomAttribute(ti, typeof(SingletonAttribute)) != null)
                {
                    serviceCollection.AddSingleton((Type)ti);

                    foreach (Type implementedInterface in ti.ImplementedInterfaces)
                    {
                        serviceCollection.AddSingleton(implementedInterface, ti);
                    }
                }
            }
        }
    }
}