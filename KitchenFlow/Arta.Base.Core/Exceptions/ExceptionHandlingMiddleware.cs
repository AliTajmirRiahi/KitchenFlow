using System.Net;
using System.Text.Json;
using Arta.Base.Core.ApiResults;
using Arta.Base.Core.Exceptions;
using Microsoft.AspNetCore.Http;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ErrorResponse response;
        HttpStatusCode statusCode;

        if (exception is ValidatorExecption validationEx)
        {
            statusCode = validationEx.StatusCode;
            response = new ErrorResponse
            {
                Error = new ErrorDetail
                {
                    Code = validationEx.ErrorCode,
                    Message = validationEx.Message,
                    Target = "ModelValidation",
                    Details = validationEx.ValidationResult.Errors
                        .Select(e => new ErrorSubDetail
                        {
                            Code = "ValidationFailure",
                            Target = e.PropertyName,
                            Message = e.ErrorMessage
                        }).ToList()
                }
            };
        }
        else if (exception is BaseException baseEx)
        {
            statusCode = baseEx.StatusCode;
            response = new ErrorResponse
            {
                Error = new ErrorDetail
                {
                    Code = baseEx.ErrorCode,
                    Message = baseEx.Message,
                    Target = "Application",
                    Details = new List<ErrorSubDetail>()
                }
            };
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            response = new ErrorResponse
            {
                Error = new ErrorDetail
                {
                    Code = "UnhandledError",
                    Message = "An unexpected error occurred.",
                    Target = "Server",
                    Details = new List<ErrorSubDetail>()
                }
            };
        }

        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
