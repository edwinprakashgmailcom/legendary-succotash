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
            //Set 5 min as the lifetime for the HttpMessageHandler objects in the pool
            .SetHandlerLifetime(TimeSpan.FromSeconds(5))
            .AddPolicyHandler(GetRetryPolicy());
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
