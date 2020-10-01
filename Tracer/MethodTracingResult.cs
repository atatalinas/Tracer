using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Tracer
{
    [DataContract(Name = "method")]
    public class MethodTracingResult
    {
        //Таймер 
        private Stopwatch timer;

        //Список внутренних методов
        private List<MethodTracingResult> innerMethods;

        //поля с именем класса и метода
        [DataMember(Name = "name", Order = 0)]
        public string MethodName { get; set; }

        [DataMember(Name = "class", Order = 1)]
        public string ClassName { get; set; }

        //время выполнения
        public long ExecutionTime
        {
            get
            {
                return timer.ElapsedMilliseconds;
            }
        }

        //строковое представление времени
        [DataMember(Name = "time", Order = 2)]
        public string ExecutionTimeString
        {
            get
            {
                return ExecutionTime.ToString() + "ms";
            }
            private set
            {
                //для сериализации
            }
        }

        [DataMember(Name = "methods", Order = 3)]
        public List<MethodTracingResult> InnerMethods
        {
            get
            {
                return new List<MethodTracingResult>(innerMethods);
            }
            private set
            {
                // для сериализации
            }
        }

        public MethodTracingResult()
        {
            innerMethods = new List<MethodTracingResult>();
            timer = new Stopwatch();
        }

        public void addInnerMethod(MethodTracingResult method)
        {
            innerMethods.Add(method);
        }

        //Методы для работы с таймером

        public void startTracing()
        {
            timer.Start();
        }

        public void stopTracing()
        {
            timer.Stop();
        }

    }
}
