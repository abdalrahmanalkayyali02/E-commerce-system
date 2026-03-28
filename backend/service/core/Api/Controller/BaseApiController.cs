using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return result.Match(
            value => Ok(new
            {
                Data = value,
                Error = (string)null,
                IsSuccess = true,
                IsFail = false,
                Timestamp = result.Timestamp
            }),
            errors =>
            {
                var firstError = errors[0];

                var statusCode = firstError.Type switch
                {
                    ErrorKind.Validation => StatusCodes.Status400BadRequest,
                    ErrorKind.NotFound => StatusCodes.Status404NotFound,
                    ErrorKind.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };

                return StatusCode(statusCode, new
                {
                    Data = (object)null,
                    Errors = errors.Select(e => new  
                    {
                        e.Code,
                        e.Description,
                    }),
                    IsSuccess = false,
                    IsFail = true,
                    Timestamp = result.Timestamp
                });
            }
        );
    }
}