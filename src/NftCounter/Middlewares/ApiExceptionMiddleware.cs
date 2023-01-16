using Microsoft.AspNetCore.Mvc;
using NftCounter.Api.Infrastructure.Exceptions;
using System.Net;

namespace NftCounter.Api.ApiMiddlewares;

public sealed class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, 
        IHostEnvironment hostEnvironment,
        ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        _logger.LogError(e, "Exception occured - endpoint {endpoint}", context.Request.Path);
        var statusCode = GetStatusCode(e);

        //RFC 7807
        ProblemDetails problemDetails = new()
        {
            Status = statusCode,
            Title = _hostEnvironment.IsDevelopment() ? e.Message : "NFT counter server error",
            Detail = _hostEnvironment.IsDevelopment() ? e.StackTrace : "Please contact support.",
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private int GetStatusCode(Exception e) {
        return e switch
        {
            GraphQlException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}
