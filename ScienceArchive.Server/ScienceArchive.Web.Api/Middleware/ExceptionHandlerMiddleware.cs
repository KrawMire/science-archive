using System.Text.Json;
using ScienceArchive.Core.Exceptions;
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
            await ProcessException(httpContext, ex.Message, 400);
        }
        catch (WrongCredentialsException)
        {
            await ProcessException(httpContext, "Wrong credentials were provided", 400);
        }
        catch (WrongConfirmationCodeException)
        {
            await ProcessException(httpContext, "Wrong confirmation code", 400);
        }
        catch (InvalidFieldValueException ex)
        {
            await ProcessException(httpContext, $"Invalid value of field: {ex.InvalidFieldName}", 400);
        }
        catch (EntityNotFoundException ex)
        {
            await ProcessException(httpContext, $"{ex.EntityName} was not found", 400);
        }
        catch (DuplicateLoginException)
        {
            await ProcessException(httpContext, $"User with such login already exists", 400);
        }
        catch (DuplicateEmailException)
        {
            await ProcessException(httpContext, $"User with such email already exists", 400);
        }
        catch (Exception ex)
        {
            await ProcessException(httpContext, "Unhandled error occurred", 500, ex);
        }
    }

    private async Task ProcessException(HttpContext httpContext, string message, int statusCode, Exception? ex = null)
    {
        var response = new ErrorResponse(message);
        var body = JsonSerializer.Serialize(response);

        if (ex is not null)
        {
            _logger.LogError($"{ex.Message}, {ex.StackTrace}");
        }
        
        try
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(body);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while writing response: {e.Message}");
        }
    }
}
