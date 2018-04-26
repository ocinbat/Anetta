using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Anetta.Interception
{
    public class InterceptedServiceProvider : IServiceProvider
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly IServiceProvider _decorated;
        private readonly List<Type> _interceptedTypes = new List<Type>();

        public InterceptedServiceProvider(IServiceProvider decorated)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ScanForInterceptedMethodTypesInAssembly(entryAssembly);
            _decorated = decorated;
        }

        public object GetService(Type serviceType)
        {
            if (_interceptedTypes.Contains(serviceType))
            {
                var attributes = serviceType
                    .GetMethods()
                    .SelectMany(y => y.GetCustomAttributes().OfType<MethodInterceptorAttribute>())
                    .ToList();

                List<IInterceptor> interceptors = new List<IInterceptor>();

                foreach (MethodInterceptorAttribute attribute in attributes)
                {
                    interceptors.Add(attribute);
                }

                return ProxyGenerator.CreateClassProxy(serviceType, interceptors.ToArray());
            }

            return _decorated.GetService(serviceType);
        }

        private void ScanForInterceptedMethodTypesInAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                Dictionary<string, MethodInfo> methods = type
                    .GetMethods()
                    .Where(y => y.GetCustomAttributes().OfType<MethodInterceptorAttribute>().Any())
                    .ToDictionary(z => z.Name);

                if (methods.Count == 0)
                {
                    continue;
                }

                _interceptedTypes.Add(type);
            }
        }
    }
}