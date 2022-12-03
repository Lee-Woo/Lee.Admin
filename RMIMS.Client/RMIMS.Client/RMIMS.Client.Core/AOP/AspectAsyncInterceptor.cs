using Castle.DynamicProxy;
using DryIoc;
using Microsoft.Extensions.Logging;
using RMIMS.Client.Core.AOP.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RMIMS.Client.Core.AOP
{
    public class AspectAsyncInterceptor : AsyncInterceptorBase
    {
        private readonly IContainer _container;

        public AspectAsyncInterceptor(IContainer container)
        {
            _container = container;
        }
        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var aspects = GetAspects(invocation);

            var args = new AspectArgs(_container, invocation);          

            Exception ex = null;

            try
            {
                foreach (var aspect in aspects)
                {
                    aspect.OnEntry(args);
                }

                var result = await proceed(invocation, proceedInfo).ConfigureAwait(false);
                return result;
            }

            catch (Exception exception)
            {
                ex = exception;
                //return (TResult)(object)ex.Message;
                return default(TResult);
            }

            finally
            {
                foreach (var aspect in aspects.Reverse())
                {
                    if (ex != null)
                    {
                        aspect.OnException(args, ex);
                    }

                    else
                    {
                        aspect.OnSucess(args);
                    }

                    aspect.OnExit(args);
                }
            }
        }
        private static IList<Aspect> GetAspects(IInvocation invocation)
        {
            return invocation.TargetType.GetAspects()
                .Union(invocation.MethodInvocationTarget.GetAspects())
                .ToList();
        }

        protected async override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            try
            {
                Debug.WriteLine($"{invocation.Method.Name}:Starting Void Method");
                await proceed(invocation, proceedInfo).ConfigureAwait(false);
                Debug.WriteLine($"{invocation.Method.Name}:Completed Void  Method");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{invocation.Method.Name}:Throw Exception :{e.Message}");
                throw;
            }
        }
    }
}
