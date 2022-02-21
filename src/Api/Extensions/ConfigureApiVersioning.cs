namespace Companies.Api.Extensions;

public static class ConfigureApiVersioning
{
    public static void AddApiVersioningService(this IServiceCollection services)
    {
        services.AddApiVersioning();

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
