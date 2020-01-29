using System;
using Anetta.Configuration;
using Anetta.Extensions;
using Anetta.ServiceConfiguration;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.AddAnnotations();
            services.AddConfigurations(configuration);
            services.AddServiceConfigurators(configuration);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            SampleServiceWithAttribute sampleServiceWithAttribute = serviceProvider.GetService<SampleServiceWithAttribute>();
            sampleServiceWithAttribute.Execute();

            SampleServiceWithConfigurator sampleServiceWithConfigurator = serviceProvider.GetService<SampleServiceWithConfigurator>();
            sampleServiceWithConfigurator.Execute();

            Console.ReadLine();
        }
    }
}
