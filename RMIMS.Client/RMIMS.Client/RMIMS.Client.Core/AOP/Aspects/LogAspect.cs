using System;
using System.Diagnostics;

namespace RMIMS.Client.Core.AOP.Aspects
{
    public class LogAspect : Aspect
    {
        public override void OnEntry(AspectArgs args)
        {
            Debug.WriteLine("Log OnEntry");
        }

        public override void OnExit(AspectArgs args)
        {
            Debug.WriteLine("Log OnExit");
        }
    }
}
