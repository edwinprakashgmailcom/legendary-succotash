using Companies.Core;
using Companies.Core.Interfaces;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Companies.Api.Extensions;

public static class ConfigureCore
{
    public static void AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<BaseUrlConfiguration>(
            configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME));
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddHttpClient<ICompanyQueryService, CompanyQueryService>()
            .AddPolicyHandler(GetRetryPolicy());
    }

    /// <summary>
    /// Retry policy for network failures, request timeouts and HTTP 5XX status codes
    /// </summary>
    /// <returns></returns>
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
