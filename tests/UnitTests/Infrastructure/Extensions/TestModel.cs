using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Companies.UnitTests.Infrastructure.Extensions;

public class TestModel : IEquatable<TestModel>
{
    [XmlElement("id")]
    public int Id { get; set; }

    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }


    public bool Equals([AllowNull] TestModel other) =>
        other?.Id == Id && other?.Name == Name && other?.Description == Description;
}
