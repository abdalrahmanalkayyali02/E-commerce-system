using Common.Impl.Result;
using Common.Result;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace Api.Middleware;

public class GlobalResultMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalResultMiddleware> _logger;

    public GlobalResultMiddleware(RequestDelegate next, ILogger<GlobalResultMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 1. Let the request continue through the pipeline
            await _next(context);

            // 2. Intercept if the Status is an error AND no body has been written yet
            // This catches 415, 401, 403, 404
            if (!context.Response.HasStarted && (context.Response.StatusCode < 200 || context.Response.StatusCode >= 300))
            {
                await HandleFrameworkErrorAsync(context);
            }
        }
        catch (Exception ex)
        {
            // 3. Catch Database crashes or code exceptions (500)
            _logger.LogError(ex, "An unhandled exception occurred during the request.");
            await HandleExceptionAsync(context);
        }
    }

    private static async Task HandleFrameworkErrorAsync(HttpContext context)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        // Map the Status Code to your Error Kind factories
        var error = context.Response.StatusCode switch
        {
            StatusCodes.Status401Unauthorized => Error.Unauthorized("Auth.Unauthorized", "You are not authorized. Please provide a valid token."),
            StatusCodes.Status403Forbidden => Error.Forbidden("Auth.Forbidden", "You do not have permission to access this resource."),
            StatusCodes.Status404NotFound => Error.NotFound("Route.NotFound", "The requested endpoint does not exist."),
            StatusCodes.Status415UnsupportedMediaType => Error.Validation("Media.Unsupported", "The media format is not supported. Use application/json."),
            _ => Error.Failure("Request.Error", $"The request failed with status code {context.Response.StatusCode}")
        };

        // Wrap in your Result Pattern
        var result = Result<object>.Failure(error);
        await context.Response.WriteAsJsonAsync(result);
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        // Use the Unexpected factory for crashes
        var error = Error.Unexpected("Server.Exception", "An internal server error occurred. Please try again later.");
        var result = Result<object>.Failure(error);

        await context.Response.WriteAsJsonAsync(result);
    }
}