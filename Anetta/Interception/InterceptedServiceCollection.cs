using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Anetta.Interception
{
    public class InterceptedServiceCollection : IServiceCollection
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly IServiceCollection _decorated;

        public InterceptedServiceCollection(IServiceCollection decorated)
        {
            List<ServiceDescriptor> newServiceDescriptors = new List<ServiceDescriptor>();

            foreach (ServiceDescriptor descriptor in decorated)
            {
                var attributes = descriptor.ImplementationType
                    .GetMethods()
                    .SelectMany(y => y.GetCustomAttributes().OfType<MethodInterceptorAttribute>())
                    .ToList();

                if (attributes.Count > 0)
                {
                    foreach (MethodInterceptorAttribute attribute in attributes)
                    {
                        newServiceDescriptors.Add(new ServiceDescriptor(attribute.GetType(), attribute.GetType()));
                    }
                }
            }

            foreach (ServiceDescriptor descriptor in newServiceDescriptors)
            {
                decorated.Add(descriptor);
            }

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
    }
}