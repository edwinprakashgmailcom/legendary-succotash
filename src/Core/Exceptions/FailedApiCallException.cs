using System.Net;

namespace Companies.Core.Exceptions;

public class FailedApiCallException : Exception
{
    public FailedApiCallException(HttpMethod method, string resourceUrl, HttpStatusCode status) 
        : base($"{method} {resourceUrl} failed with the status {status}.")
    {
    }
}
