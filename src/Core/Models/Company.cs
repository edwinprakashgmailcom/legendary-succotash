using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Companies.Core.Models;

/// <summary>
/// Aggregate root of the domain
/// </summary>
public class Company
{
    [JsonPropertyName("id")]
    [XmlElement("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    [XmlElement("description")]
    public string Description { get; set; }
}
