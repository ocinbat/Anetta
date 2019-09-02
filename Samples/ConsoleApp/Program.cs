using System;
using Anetta.Extensions;
using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddAnnotations();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            SampleServiceWithAttribute sampleServiceWithAttribute = serviceProvider.GetService<SampleServiceWithAttribute>();
            sampleServiceWithAttribute.Execute();

            Console.ReadLine();
        }
    }
}
