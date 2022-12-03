using Castle.DynamicProxy;
using DryIoc;
using System;

namespace RMIMS.Client.Core.AOP.Aspects
{
    public class AspectArgs
    {
        public IContainer Container { get; private set; }
        public IInvocation Invocation { get; private set; }

        public AspectArgs(IContainer container, IInvocation invocation)
        {
            Invocation = invocation;
            Container = container;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class Aspect : Attribute
    {
        public virtual void OnEntry(AspectArgs args)
        {
        }

        public virtual void OnExit(AspectArgs args)
        {
        }

        public virtual void OnSucess(AspectArgs args)
        {
        }

        public virtual void OnException(AspectArgs args, Exception exception)
        {
        }
    }
}
