using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer: ITracer
    {
        protected TraceResult traceResult;

        public void StartTrace()
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            MethodTracingResult methodResult = new MethodTracingResult
            {
                ClassName = methodBase.ReflectedType.Name,
                MethodName = methodBase.Name
            };
            ThreadTracingResult curThreadResult = traceResult.AddThreadResult(Thread.CurrentThread.ManagedThreadId);
            curThreadResult.StartTracingMethod(methodResult);
        }

        public void StopTrace()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            traceResult.GetThreadResult(threadID).StopTracingMethod();
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public Tracer()
        {
            traceResult = new TraceResult();
        }
    }
}
