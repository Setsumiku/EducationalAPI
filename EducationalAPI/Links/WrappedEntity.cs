using System.Collections;
using System.Dynamic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace EducationalAPI.Links
{
    public class WrappedEntity : DynamicObject
    {
        private readonly string root = "EntityWithLinks";
        private readonly IDictionary<string, object> expando = null;

        public WrappedEntity()
        {
            expando = new ExpandoObject();
        }


        private void WriteXmlElement(string key, object value, XmlWriter writer)
        {
            writer.WriteStartElement(key);

            if (value.GetType() == typeof(List<Link>))
            {
                foreach (var val in value as List<Link>)
                {
                    writer.WriteStartElement(nameof(Link));
                    WriteXmlElement(nameof(val.Href), val.Href, writer);
                    WriteXmlElement(nameof(val.Method), val.Method, writer);
                    WriteXmlElement(nameof(val.Rel), val.Rel, writer);
                    writer.WriteEndElement();
                }
            }
            else
            {
                writer.WriteString(value.ToString());
            }

            writer.WriteEndElement();
        }

        public void Add(string key, object value)
        {
            expando.Add(key, value);
        }



    }
}
