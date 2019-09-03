using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anetta.ServiceConfiguration
{
    public interface IServiceConfigurator
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}