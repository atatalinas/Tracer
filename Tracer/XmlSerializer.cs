using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace Tracer
{
    public class XmlSerializer: ISerializer
    {
        private XmlWriterSettings settings;

        protected readonly DataContractSerializer xmlSerializer;

        public void Serialize(TraceResult result, Stream stream)
        {
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                xmlSerializer.WriteObject(writer, result);
            }
            
        }

        public XmlSerializer()
        {
            //настройка отступов
            settings = new XmlWriterSettings { Indent = true };
            xmlSerializer = new DataContractSerializer(typeof(TraceResult));
        }

    }
}
