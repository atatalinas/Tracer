using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    [DataContract(Name = "thread")]
    public class ThreadTracingResult
    {
        //Стек для хранения вложенных методов
        private Stack<MethodTracingResult> threadMethods;
        //Список методов потока
        private List<MethodTracingResult> tracedMethods;

        //ID потока
        [DataMember(Name = "id", Order = 0)]
        public int ThreadID { get; set; }

        //Время выполнения всех методов
        public long ExecutionTime
        {
            get
            {
                //Получаем время выполнения всех методов
                long execTime = 0;
                foreach (MethodTracingResult methodResult in tracedMethods)
                {
                    execTime += methodResult.ExecutionTime;
                }
                return execTime;
            }
        }

        //Строковое представление времени выполнения
        [DataMember(Name = "time", Order = 1)]
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

        //Добавление измеряемого метода в список
        protected void AddMethod(MethodTracingResult methodResult)
        {
            //если стек методов не пустой
            if (threadMethods.Count > 0)
            {
                threadMethods.Peek().addInnerMethod(methodResult);
            }
            else
            {
                tracedMethods.Add(methodResult);
            }
            threadMethods.Push(methodResult);
        }

        //Начало замера
        public void StartTracingMethod(MethodTracingResult methodResult)
        {
            AddMethod(methodResult);
            methodResult.startTracing();
        }

        //Конец замера
        public void StopTracingMethod()
        {
            if(threadMethods.Count != 0)
            {
                threadMethods.Pop().stopTracing();
            }
        }

        //Доступ к списку методов
        [DataMember(Name = "methods", Order = 2)]
        public List<MethodTracingResult> ThreadMethods
        {
            get
            {
                return new List<MethodTracingResult>(tracedMethods);
            }
            private set
            {
                //сериализация
            }
        }
       
        public ThreadTracingResult(int id)
        {
            threadMethods = new Stack<MethodTracingResult>();
            tracedMethods = new List<MethodTracingResult>();
            ThreadID = id;
        }
    }
}
