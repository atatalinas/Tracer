using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    // Запись результатов в файл
    public class FileWriter : IWriter
    {
        public string filename { get; set; }

        public void WriteResult(TraceResult result, ISerializer serializer)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Append))
            {
                serializer.Serialize(result, stream);
            }
        }

        public FileWriter(string fileName)
        {
            this.filename = fileName;
        }
    }
}
