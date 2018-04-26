using System;
using Anetta.Interception;
using Castle.DynamicProxy;

namespace ConsoleApp.Interceptors
{
    public class LogExecutionTime : MethodInterceptorAttribute
    {
        public override void Intercept(IInvocation invocation)
        {
            Console.WriteLine("LogExecutionTime çalıştı.");
            invocation.Proceed();
        }
    }
}