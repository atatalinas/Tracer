using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.IO;


namespace Tracer
{
    public class JsonSerializer: ISerializer
    {
        private DataContractJsonSerializer serializer;

        public void Serialize(TraceResult result, Stream stream)
        {
            using (XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: true, indent: true))
            {
                serializer.WriteObject(writer, result);
            }
        }
        public JsonSerializer()
        {
            serializer = new DataContractJsonSerializer(typeof(TraceResult));
        }
        
    }
}
