using System;
using Anetta.Abstractions;
using ConsoleApp.Interceptors;

namespace ConsoleApp.Services
{
    public class SampleServiceWithInterface : ISingleton
    {
        [LogExecutionTime]
        public void Execute()
        {
            Console.WriteLine("SampleServiceWithInterface is executed.");
        }
    }
}