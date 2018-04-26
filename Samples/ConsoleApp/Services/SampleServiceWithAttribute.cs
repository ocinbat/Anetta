using System;
using Anetta.Attributes;
using ConsoleApp.Interceptors;

namespace ConsoleApp.Services
{
    [Transient]
    public class SampleServiceWithAttribute
    {
        [LogExecutionTime]
        public virtual void Execute()
        {
            Console.WriteLine("SampleServiceWithAttribute is executed.");
        }
    }
}