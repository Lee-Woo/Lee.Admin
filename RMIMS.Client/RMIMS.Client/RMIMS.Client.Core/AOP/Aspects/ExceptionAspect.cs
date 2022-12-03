using DryIoc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace RMIMS.Client.Core.AOP.Aspects
{
    public class ExceptionAspect : Aspect
    {
        public override void OnEntry(AspectArgs args)
        {
            base.OnEntry(args);

            var logger = GetLogger(args);
            logger.LogInformation($"OnEntry Invocation:{args.Invocation}");
            Debug.WriteLine($"OnEntry Invocation:{args.Invocation}");
        }
        public override void OnException(AspectArgs args, Exception exception)
        {
            var logger = GetLogger(args);
            logger.LogError($"OnException Invocation:{args.Invocation}, Message: {exception.Message}");
            Debug.WriteLine("OnException:{0} ", new string[] { exception.Message });

            args.Invocation.ReturnValue = false;
        }
        public override void OnExit(AspectArgs args)
        {
            base.OnExit(args);

            var logger = GetLogger(args);
            logger.LogInformation($"OnExit Invocation:{args.Invocation}");
            Debug.WriteLine($"OnExit Invocation:{args.Invocation}");
        }

        public ILogger GetLogger(AspectArgs args)
        {
            IContainer container = args.Container;
            if (container != null)
            {
                return container.Resolve<ILogger>();
            }
            return null;
        }

    }
}
