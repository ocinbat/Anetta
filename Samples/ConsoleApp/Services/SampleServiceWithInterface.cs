using System;
using Anetta.Attributes;

namespace ConsoleApp.Services
{
    [Scoped]
    public class SampleServiceWithInterface
    {
        public void Execute()
        {
            Console.WriteLine("SampleServiceWithInterface is executed.");
        }
    }
}