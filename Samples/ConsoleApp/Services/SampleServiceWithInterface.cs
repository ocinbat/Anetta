using System;
using Anetta.Abstractions;

namespace ConsoleApp.Services
{
    public class SampleServiceWithInterface : ISingleton
    {
        public void Execute()
        {
            Console.WriteLine("SampleServiceWithInterface is executed.");
        }
    }
}