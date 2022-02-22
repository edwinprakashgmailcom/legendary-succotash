namespace Companies.Api.Extensions;

public static class ConfigureCors
{

    public const string CORS_POLICY = "CorsPolicy";

    public static void AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: CORS_POLICY,
                              builder =>
                              {
                                  builder.WithOrigins("*");
                                  builder.AllowAnyMethod();
                                  builder.AllowAnyHeader();
                              });
        });
    }
}
