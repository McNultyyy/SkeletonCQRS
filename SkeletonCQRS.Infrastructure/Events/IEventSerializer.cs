using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SkeletonCQRS.Infrastructure.Events
{
    public interface IEventSerializer
    {
        string Serialize<TEvent>(TEvent e) where TEvent : IDomainEvent;

        TEvent Deserialize<TEvent>(string obj) where TEvent : IDomainEvent;
        IDomainEvent Deserialize(string obj, Type type);
    }

    public class EventXmlSerializer : IEventSerializer
    {
        public string Serialize<TEvent>(TEvent e)
            where TEvent : IDomainEvent
        {
            var serializer = new XmlSerializer(e.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (var sww = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sww))
                {
                    serializer.Serialize(xw, e, ns);
                    return sww.ToString();
                }
            }
        }

        public TEvent Deserialize<TEvent>(string obj)
            where TEvent : IDomainEvent
        {
            var untypedResult = Deserialize(obj, typeof(TEvent));
            var result = (TEvent)untypedResult;
            return result;
        }

        public IDomainEvent Deserialize(string obj, Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var s = new StringReader(obj))
            {
                var result = (IDomainEvent)serializer.Deserialize(s);
                return result;
            }
        }
    }
}