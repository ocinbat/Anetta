using System;
using Anetta.Attributes;

namespace ConsoleApp.Services
{
    [Transient]
    public class SampleServiceWithAttribute
    {
        public void Execute()
        {
            Console.WriteLine("SampleServiceWithAttribute is executed.");
        }
    }
}