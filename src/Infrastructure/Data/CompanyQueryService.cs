using Companies.Core;
using Companies.Core.Exceptions;
using Companies.Core.Interfaces;
using Companies.Core.Models;
using Companies.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using System.Net;

namespace Companies.Infrastructure.Data;

public class CompanyQueryService : ICompanyQueryService
{
        private readonly HttpClient _httpClient;

    public CompanyQueryService(HttpClient httpClient,
        IOptions<BaseUrlConfiguration> baseUrlConfiguration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(baseUrlConfiguration.Value.CustomerXmlApiBase);
    }

    public async Task<Company?> GetByIdAsync(int companyId)
    {
        var resourceUri = new Uri($"/MiddlewareNewZealand/evaluation-instructions/main/xml-api/{companyId}.xml", UriKind.Relative);

        var response = await _httpClient.GetAsync(resourceUri);

        if (response.IsSuccessStatusCode)
        {
            var xmlContent = await response.Content.ReadAsStringAsync();
            return xmlContent.FromXml<Company>("Data");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        throw new FailedApiCallException(HttpMethod.Get, resourceUri.OriginalString, response.StatusCode);
    }
}
