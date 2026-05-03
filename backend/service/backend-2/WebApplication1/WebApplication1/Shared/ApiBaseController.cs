using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {

        protected ActionResult HandleResult<T>(IResult<T> result)
        {
            var firstError = result.Error.FirstOrDefault();

            var response = new Dictionary<string, object?>
        {
            { "isSuccess", result.IsSuccess },
            { "timestamp", DateTime.UtcNow },
            { "statusCode", result.StatusCode },
            { "traceId", Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        };

            if (result.IsSuccess)
            {
                response.Add("value", result.Value);
            }
            else if (result.IsFailure)
            {
                if (firstError != null)
                {
                    response.Add("errors", new
                    {
                        title = firstError.Title,
                        type = GetTypeUri(firstError.Type),
                        detail = firstError.Description,
                        instance = HttpContext.Request.Path,
                        code = firstError.Code
                    });
                }
            }

            return StatusCode(result.StatusCode, response);
        }

        private static string GetTypeUri(ErrorKind? type) => type switch
        {
            ErrorKind.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorKind.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorKind.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorKind.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
    }
}
