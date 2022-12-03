using System;
using System.Diagnostics;

namespace RMIMS.Client.Core.AOP.Aspects
{
    public class TimingAspect : Aspect
    {
        Stopwatch _watch;

        public override void OnEntry(AspectArgs args)
        {
            Debug.WriteLine("Timing Start...");

            _watch = Stopwatch.StartNew();
        }

        public override void OnExit(AspectArgs args)
        {
            _watch.Stop();

            Debug.WriteLine("Timing Stop: {0}", _watch.Elapsed);
        }
    }
}
