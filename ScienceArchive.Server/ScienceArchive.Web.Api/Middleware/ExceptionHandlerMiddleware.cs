using System.Net;
using System.Text.Json;
using ScienceArchive.Web.Api.Responses;

namespace ScienceArchive.Web.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BadHttpRequestException ex)
        {
            await ProcessException(ex, httpContext, 400);
        }
        catch (Exception ex)
        {
            await ProcessException(ex, httpContext);
        }
    }

    private async Task ProcessException(Exception ex, HttpContext httpContext, int statusCode = 500)
    {
        var response = new ErrorResponse(ex.Message);
        var body = JsonSerializer.Serialize(response);

        try
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(body);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}
