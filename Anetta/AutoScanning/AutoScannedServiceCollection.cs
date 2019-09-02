using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Anetta.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.AutoScanning
{
    public class AutoScannedServiceCollection : IServiceCollection
    {
        private readonly IServiceCollection _decorated;

        public AutoScannedServiceCollection(IServiceCollection decorated)
        {
            ScanAndRegisterAllAssemlies(decorated);
            _decorated = decorated;
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _decorated.GetEnumerator();
        }

        public void Add(ServiceDescriptor item)
        {
            _decorated.Add(item);
        }

        public void Clear()
        {
            _decorated.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _decorated.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _decorated.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _decorated.Remove(item);
        }

        public int Count => _decorated.Count;
        public bool IsReadOnly => _decorated.IsReadOnly;
        public int IndexOf(ServiceDescriptor item)
        {
            return _decorated.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _decorated.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _decorated.RemoveAt(index);
        }

        public ServiceDescriptor this[int index]
        {
            get => _decorated[index];
            set => _decorated[index] = value;
        }

        private void ScanAndRegisterAllAssemlies(IServiceCollection serviceCollection)
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

        private void ScanAndRegisterAssemblyByAttributes(Assembly assembly, IServiceCollection serviceCollection)
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