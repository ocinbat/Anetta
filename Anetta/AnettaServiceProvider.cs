using System;
using System.Linq;
using System.Reflection;
using Anetta.Abstractions;
using Anetta.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta
{
    public class AnettaServiceProvider : IServiceProvider
    {
        private readonly ServiceProvider _provider;

        public AnettaServiceProvider(IServiceCollection serviceCollection)
        {
            ScanAndRegisterAllAssemlies(serviceCollection);
            _provider = serviceCollection.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        private void ScanAndRegisterAllAssemlies(IServiceCollection serviceCollection)
        {
            // TODO: Implement a smarter assembly scanner.
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ScanAndRegisterAssemblyByInterfaceAbstractions(entryAssembly, serviceCollection);
            ScanAndRegisterAssemblyByAttributes(entryAssembly, serviceCollection);

            foreach (AssemblyName assemblyName in entryAssembly.GetReferencedAssemblies())
            {
                Assembly assembly = Assembly.Load(assemblyName);
                ScanAndRegisterAssemblyByInterfaceAbstractions(assembly, serviceCollection);
                ScanAndRegisterAssemblyByAttributes(assembly, serviceCollection);
            }
        }

        private void ScanAndRegisterAssemblyByInterfaceAbstractions(Assembly assembly, IServiceCollection serviceCollection)
        {
            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(ITransient)))
                {
                    serviceCollection.AddTransient((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(ITransient)))
                    {
                        serviceCollection.AddTransient(implementedInterface, ti);
                    }
                }

                if (ti.ImplementedInterfaces.Contains(typeof(IScoped)))
                {
                    serviceCollection.AddScoped((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(IScoped)))
                    {
                        serviceCollection.AddScoped(implementedInterface, ti);
                    }
                }

                if (ti.ImplementedInterfaces.Contains(typeof(ISingleton)))
                {
                    serviceCollection.AddSingleton((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(ISingleton)))
                    {
                        serviceCollection.AddSingleton(implementedInterface, ti);
                    }
                }
            }
        }

        private void ScanAndRegisterAssemblyByAttributes(Assembly assembly, IServiceCollection serviceCollection)
        {
            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (Attribute.GetCustomAttribute(ti, typeof(TransientAttribute)) != null)
                {
                    serviceCollection.AddTransient((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(ITransient)))
                    {
                        serviceCollection.AddTransient(implementedInterface, ti);
                    }
                }

                if (Attribute.GetCustomAttribute(ti, typeof(ScopedAttribute)) != null)
                {
                    serviceCollection.AddScoped((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(IScoped)))
                    {
                        serviceCollection.AddScoped(implementedInterface, ti);
                    }
                }

                if (Attribute.GetCustomAttribute(ti, typeof(SingletonAttribute)) != null)
                {
                    serviceCollection.AddSingleton((Type)ti);
                    foreach (Type implementedInterface in ti.ImplementedInterfaces.Where(i => i != typeof(ISingleton)))
                    {
                        serviceCollection.AddSingleton(implementedInterface, ti);
                    }
                }
            }
        }
    }
}