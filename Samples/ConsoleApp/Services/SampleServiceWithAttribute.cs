using System;
using Anetta.Attributes;

namespace ConsoleApp.Services
{
    [Singleton]
    public class SampleServiceWithAttribute
    {
        public void Execute()
        {
            Console.WriteLine("SampleServiceWithAttribute is executed.");
        }
    }
}