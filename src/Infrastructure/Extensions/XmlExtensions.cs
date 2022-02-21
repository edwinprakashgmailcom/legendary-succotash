using System.Xml.Serialization;

namespace Companies.Infrastructure.Extensions
{
    public static class XmlExtensions
    {
        public static T FromXml<T>(this string xmlContent, string rootAttribute)
        {
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootAttribute));

            using var reader = new StringReader(xmlContent);
            return (T)serializer.Deserialize(reader);
        }
    }
}
