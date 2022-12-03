using Castle.DynamicProxy;
using DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.DryIoc
{
    public class AsyncInterceptorAdaper<T> : AsyncDeterminationInterceptor where T : IAsyncInterceptor
    {
        public AsyncInterceptorAdaper(T asyncInterceptor) : base(asyncInterceptor)
        {
        }
    }
}
