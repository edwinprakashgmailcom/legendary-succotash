namespace Companies.Core;

/// <summary>
/// Represents the configuration for the base urls of external (to this app) apis
/// </summary>
public class BaseUrlConfiguration
{
    public const string CONFIG_NAME = "BaseUrls";

    public string CustomerXmlApiBase { get; set; }
}
