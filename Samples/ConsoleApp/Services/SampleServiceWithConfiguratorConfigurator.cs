using Anetta.ServiceConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Services
{
    public class SampleServiceWithConfiguratorConfigurator : IServiceConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SampleServiceWithConfigurator>();
        }
    }
}