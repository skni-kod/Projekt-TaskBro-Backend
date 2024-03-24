using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message = "";

        var exceptionType = e.GetType();

        if (exceptionType.Name == nameof(NotFoundException))
        {
            message = e.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = e.StackTrace;
        }
        else if (exceptionType.Name == nameof(BadRequestException))
        {
            message = e.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = e.StackTrace;
        }
        else if (exceptionType.Name == nameof(Conflict))
        {
            message = e.Message;
            status = HttpStatusCode.Conflict;
            stackTrace = e.StackTrace;
        }
        else
        {
            message = e.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = e.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}