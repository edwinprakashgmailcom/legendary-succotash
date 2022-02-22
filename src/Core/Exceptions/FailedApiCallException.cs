using System.Net;

namespace Companies.Core.Exceptions;

/// <summary>
///  Represents unhandled errors that occur when making downstream Api calls
/// </summary>
public class FailedApiCallException : Exception
{
    public FailedApiCallException(HttpMethod method, string resourceUrl, HttpStatusCode status) 
        : base($"{method} {resourceUrl} failed with the status {status}.")
    {
    }
}
