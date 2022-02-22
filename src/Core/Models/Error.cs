using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Companies.Core.Models;

public class Error
{
    public Error([NotNull]string error, string description)
    {
        Summary = error;
        Description = description;
    }

    [JsonPropertyName("error")]
    public string Summary { get; set; }

    [JsonPropertyName("error_description")]
    public string Description { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
