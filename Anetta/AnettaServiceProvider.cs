using System;
using Anetta.AutoScanning;
using Anetta.Interception;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta
{
    public class AnettaServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider _provider;

        public AnettaServiceProvider(IServiceCollection serviceCollection)
        {
            serviceCollection = new AutoScannedServiceCollection(serviceCollection);
            serviceCollection = new InterceptedServiceCollection(serviceCollection);

            _provider = serviceCollection.BuildServiceProvider();
            _provider = new InterceptedServiceProvider(_provider);
        }

        public object GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }
    }
}