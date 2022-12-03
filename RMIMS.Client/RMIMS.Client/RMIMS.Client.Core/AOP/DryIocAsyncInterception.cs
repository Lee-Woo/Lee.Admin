using Castle.DynamicProxy;
using DryIoc;
using ImTools;
using Prism.Ioc;
using RMIMS.Client.Core.AOP;
using System;

namespace Prism.DryIoc
{
    public static class DryIocAsyncInterception
    {
        private static readonly Lazy<DefaultProxyBuilder> _proxyBuilder = new Lazy<DefaultProxyBuilder>(() => new DefaultProxyBuilder());
        private static readonly Lazy<ProxyGenerationOptions> ProxyOptions = new Lazy<ProxyGenerationOptions>(() => new ProxyGenerationOptions(new ProxyGenerationHook()));

        public static void Intercept<TService, TInterceptor>(this IRegistrator registrator, object serviceKey = null)
            where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof(TService);

            Type proxyType;
            if (serviceType.IsInterface())
                proxyType = _proxyBuilder.Value.CreateInterfaceProxyTypeWithTargetInterface(
                    serviceType, ImTools.ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            else if (serviceType.IsClass())
                proxyType = _proxyBuilder.Value.CreateClassProxyTypeWithTarget(
                    serviceType, ImTools.ArrayTools.Empty<Type>(), ProxyOptions.Value);
            else
                throw new ArgumentException(
                    $"{serviceType} 无法被拦截, 只有接口或者类才能被拦截");

            registrator.Register(serviceType, proxyType,
                made: Made.Of(pt => pt.PublicConstructors().FindFirst(ctor => ctor.GetParameters().Length != 0),
                    Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[]))),
                setup: Setup.DecoratorOf(useDecorateeReuse: true, decorateeServiceKey: serviceKey));
        }
        /// <summary>
        /// 链式编程，方便添加多个切面
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TInterceptor"></typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="serviceKey"></param>
        /// <returns></returns>
        public static IContainerRegistry InterceptAsync<TService, TInterceptor>(
            this IContainerRegistry containerRegistry, object serviceKey = null)
            where TInterceptor : class, IAsyncInterceptor
        {
            var container = containerRegistry.GetContainer();
            container.Intercept<TService, AsyncInterceptorAdaper<TInterceptor>>(serviceKey);
            return containerRegistry;
        }


    }
}
