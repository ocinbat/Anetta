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

            IServiceProvider serviceProvider = services.BuildAnettaServiceProvider();

            SampleServiceWithAttribute sampleServiceWithAttribute = serviceProvider.GetService<SampleServiceWithAttribute>();
            SampleServiceWithInterface sampleServiceWithInterface = serviceProvider.GetService<SampleServiceWithInterface>();
            sampleServiceWithAttribute.Execute();
            sampleServiceWithInterface.Execute();

            Console.ReadLine();
        }
    }
}
