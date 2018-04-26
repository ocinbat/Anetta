using System;
using Castle.DynamicProxy;

namespace Anetta.Interception
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class MethodInterceptorAttribute : Attribute, IInterceptor
    {
        public abstract void Intercept(IInvocation invocation);
    }
}