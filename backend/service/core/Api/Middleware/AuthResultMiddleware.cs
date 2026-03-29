using Common.Impl.Result; // Your Result namespace
using Common.Result;
using System.Net;

namespace Api.Middleware
{
    public class AuthResultMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Let the request go through the pipeline
            await _next(context);

            // 2. If it comes back with 401/403 and NO body, we fix it
            if (!context.Response.HasStarted &&
               (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                context.Response.StatusCode == (int)HttpStatusCode.Forbidden))
            {
                await WriteResultResponse(context);
            }
        }

        private static async Task WriteResultResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var statusCode = context.Response.StatusCode;

            // Map the code to your Result Pattern Error
            var error = statusCode == 401
                ? Error.Unauthorized("Auth.Unauthorized", "Invalid or expired token. Please login again.")
                : Error.Forbidden("Auth.Forbidden", "Access Denied: Admin privileges required.");

            // Create the standardized Result object
            var response = new
            {
                data = (object?)null,
                errors = new[] { error },
                isSuccess = false,
                isFail = true,
                timestamp = DateTime.UtcNow
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}