using Companies.Api.Extensions;
using Companies.Api.Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddCorsService();
builder.Services.AddApiVersioningService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenService();
builder.Services.AddResponseCompression();
builder.Services.AddHealthChecks()
    .AddCheck("Alive", () => HealthCheckResult.Healthy("I am alive."));


var app = builder.Build();

app.Logger.LogInformation("Api App created...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MWNZ companies");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(ConfigureCors.CORS_POLICY);

app.UseResponseCompression();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Logger.LogInformation("Launching Api App...");

app.Run();

public partial class Program { }