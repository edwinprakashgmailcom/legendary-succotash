using Companies.Infrastructure.Extensions;
using Xunit;

namespace Companies.UnitTests.Infrastructure.Extensions;

public class XmlExtensionsTest
{
    [
        Theory,
        InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Data><id>9</id><name>Another test</name><description>description one</description></Data>", 
                    9, "Another test", "description one"),
        InlineData("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Data><id>3124</id><name>Test Value 1</name><description>description 2</description></Data>", 
                    3124, "Test Value 1", "description 2"),
    ]
    public void CorrectlyDeserializesXml(string xml, int expectedId, string expectedName, string expectedDescription) =>
        Assert.Equal(new TestModel { Id = expectedId, Name = expectedName, Description = expectedDescription }, xml.FromXml<TestModel>("Data"));
}
