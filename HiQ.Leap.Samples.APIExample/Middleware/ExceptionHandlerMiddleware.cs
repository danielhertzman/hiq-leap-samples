using System.Net;
using System.Text.Json;
using HiQ.Leap.Samples.Domain.Exceptions;

namespace HiQ.Leap.Samples.APIExample.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (APIException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ex.StatusCode;

            var errorResponse = JsonSerializer.Serialize(new
            {
                StatusCode = (int)ex.StatusCode,
                Message = ex.Message,
            });

            _logger.LogError(ex, "{message}", ex.Message);
            await context.Response.WriteAsync(errorResponse);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/plain";
            _logger.LogError(ex, "{message}", ex.Message);
            await context.Response.WriteAsync("An internal error occurred");
        }
    }
}