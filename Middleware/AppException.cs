using TraineeManagement.api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TraineeManagement.api.Handlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail, errors) = exception switch
        {
            NotFoundException notFound =>
                (StatusCodes.Status404NotFound, "Not Found", notFound.Message, (IDictionary<string, string[]>?)null),

            BadRequestException badRequest =>
                (StatusCodes.Status400BadRequest, "Bad Request", badRequest.Message, (IDictionary<string, string[]>?)null),
            
            UnAuthorizedException unAuthorized =>
                (StatusCodes.Status401Unauthorized, "UnAuthorized", unAuthorized.Message, (IDictionary<string, string[]>?)null),

            // _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "Unexpected error", (IDictionary<string, string[]>?)null)
        };

        httpContext.Response.StatusCode = statusCode;

        object responseBody;

        if (errors is not null)
        {
            responseBody = new ValidationProblemDetails(errors)
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };
        }
        else
        {
            responseBody = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };
        }

        await httpContext.Response.WriteAsJsonAsync(responseBody, cancellationToken);

        return true;
    }
}