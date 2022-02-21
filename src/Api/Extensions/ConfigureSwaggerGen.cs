using Microsoft.OpenApi.Models;

namespace Companies.Api.Extensions;

public static class ConfigureSwaggerGen
{
    public static void AddSwaggerGenService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MWNZ companies",
                Description = "MWNZ companies Api "
            });
        });
    }
}
