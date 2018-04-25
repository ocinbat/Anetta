using System;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider BuildAnettaServiceProvider(this IServiceCollection serviceCollection)
        {
            return new AnettaServiceProvider(serviceCollection);
        }
    }
}