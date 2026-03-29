using Common.Impl.Result; // Ensure this matches your Result/Error namespace
using Common.Result;
using System.Net;

namespace Api.Middleware
{
    public class IsAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public IsAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Let the request proceed through the built-in Auth layers
            await _next(context);

            // 2. If the response hasn't started and we have a 401 or 403, we add our body
            if (!context.Response.HasStarted)
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await WriteErrorResult(context,
                        Error.Unauthorized("Auth.Unauthorized", "Invalid or missing token. Please login again."));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await WriteErrorResult(context,
                        Error.Forbidden("Auth.Forbidden", "Access Denied: You do not have the required role (e.g., Seller/Admin)."));
                }
            }
        }

        private static async Task WriteErrorResult(HttpContext context, Error error)
        {
            context.Response.ContentType = "application/json";

            // Wrap the error in your standard Result Pattern structure
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