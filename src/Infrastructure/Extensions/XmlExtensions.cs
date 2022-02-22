using System.Xml.Serialization;

namespace Companies.Infrastructure.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Deserializes a XML string to an object of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <param name="rootAttribute">Optional parameter that represents the XML root element name.</param>
        /// <returns></returns>
        public static T FromXml<T>(this string xmlContent, string rootAttribute = null)
        {
            var root = !string.IsNullOrEmpty(rootAttribute) ? new XmlRootAttribute(rootAttribute) : null;
            var serializer = new XmlSerializer(typeof(T), root);

            using var reader = new StringReader(xmlContent);
            return (T)serializer.Deserialize(reader);
        }
    }
}
