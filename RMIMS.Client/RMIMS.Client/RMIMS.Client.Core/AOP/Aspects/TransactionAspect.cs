using System;
using System.Diagnostics;

namespace RMIMS.Client.Core.AOP.Aspects
{
    public class TransactionAspect : Aspect
    {
        public override void OnEntry(AspectArgs args)
        {
            Debug.WriteLine("BeginTransaction");
        }

        public override void OnSucess(AspectArgs args)
        {
            Debug.WriteLine("Commit");
        }

        public override void OnException(AspectArgs args, Exception exception)
        {
            Debug.WriteLine("Rollback");
        }

        public override void OnExit(AspectArgs args)
        {
            Debug.WriteLine("Dispose");
        }
    }
}
