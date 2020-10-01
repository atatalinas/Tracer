using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace Tracer
{
    [DataContract(Name = "result")]
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTracingResult> threadResults;

        //свойство доступа к словарю потоков
        [DataMember(Name = "threads")]
        public List<ThreadTracingResult> ThreadTracingResults
        {
            get
            {
                return new List<ThreadTracingResult>(new SortedDictionary<int, ThreadTracingResult>(threadResults).Values);
            }
            set
            {
                //для сериализации
            }
        }

        //Добавляет или возвращает уже существующий поток
        public ThreadTracingResult AddThreadResult(int id)
        {
            ThreadTracingResult result;

            //если поток уже есть, он заносится в result
            if (!threadResults.TryGetValue(id, out result))
            {
                result = new ThreadTracingResult(id);
                threadResults[id] = result;
            }
            return result;
        }

        public ThreadTracingResult GetThreadResult(int id)
        {
            return threadResults[id];
        }

        public TraceResult()
        {
            threadResults = new ConcurrentDictionary<int, ThreadTracingResult>();
        }
    }
}


