using Domain.Enums;
using Domain.Interfaces.Response;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Handlers
{
    public static class ResponseHandler
    {
        public static IActionResult HandleResponse<T>(IResponse<T> response) where T : class
        {
            return response.Status switch
            {
                ResponseStatus.Ok => new OkObjectResult(response),
                ResponseStatus.NotFound => new NotFoundObjectResult(response),
                ResponseStatus.Error => new ObjectResult(response) { StatusCode = 500 },
                ResponseStatus.Unauthorized => new UnauthorizedObjectResult(response),
                ResponseStatus.InvalidInput => new BadRequestObjectResult(response),
                ResponseStatus.Conflict => new ConflictObjectResult(response),
                ResponseStatus.Forbidden => new ObjectResult(response) { StatusCode = 403 },
                ResponseStatus.Timeout => new ObjectResult(response) { StatusCode = 408 },
                ResponseStatus.Cancelled => new ObjectResult(response) { StatusCode = 499 },
                _ => new ObjectResult(response) { StatusCode = 500 },
            };
        }
    }
}