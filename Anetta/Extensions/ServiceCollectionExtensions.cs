using Anetta.AutoScanning;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAnnotations(this IServiceCollection serviceCollection)
        {
            return new AutoScannedServiceCollection(serviceCollection);
        }
    }
}