namespace App.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;


public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    ILogger<GlobalExceptionHandlerMiddleware> _logger;


    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogCritical(ex.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new { error = ex.Message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
