using Companies.Core.Interfaces;
using Companies.Core.Models;
using System.Net;

namespace Companies.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAppLogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, IAppLogger<ExceptionMiddleware> logger)
    {
        logger.LogError(exception, "An unexpected error has occured.");

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new Error("An unexpected error has occured.", "Please try again or contact support@helpme.please").ToString());
    }
}
