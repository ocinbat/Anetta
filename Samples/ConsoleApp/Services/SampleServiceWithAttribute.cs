using System;
using Anetta.Attributes;
using Microsoft.Extensions.Options;

namespace ConsoleApp.Services
{
    [Singleton]
    public class SampleServiceWithAttribute
    {
        private readonly IOptionsMonitor<AppSettings> _appSettings;

        public SampleServiceWithAttribute(IOptionsMonitor<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public void Execute()
        {
            Console.WriteLine("SampleServiceWithAttribute is executed.");
        }
    }
}