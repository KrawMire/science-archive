using Microsoft.AspNetCore.Http.Extensions;
using ScienceArchive.Shared.Abstractions.Logging.Gateways;
using ScienceArchive.Shared.Abstractions.Logging.Models;

namespace ScienceArchive.Rest.Api.Middleware;

public class RequestResponseLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

	public RequestResponseLoggingMiddleware(
		RequestDelegate next, 
		ILogger<RequestResponseLoggingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}
	
	public async Task Invoke(HttpContext httpContext, ILogGateway logGateway)
	{
		httpContext.Request.EnableBuffering();
		
		var origBody = httpContext.Response.Body;
		await using var memoryStream = new MemoryStream();
		httpContext.Response.Body = memoryStream;
		
		await _next(httpContext);
		
		memoryStream.Seek(0, SeekOrigin.Begin);
		var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
		memoryStream.Seek(0, SeekOrigin.Begin);

		await memoryStream.CopyToAsync(origBody);
		httpContext.Response.Body = origBody;
		
		var log = await GetRequestLog(httpContext);

		log.ResponseString = responseBody;
		
		_logger.LogInformation($"Received request: Timestamp={log.Timestamp:u}, URL={log.Url}, IP={log.Ip}, User-Agent={log.UserAgent}");
		
		_ = Task.Run(() => SendLogRequest(log, logGateway));
	}

	private async Task<RequestLog> GetRequestLog(HttpContext httpContext)
	{
		var url = httpContext.Request.GetDisplayUrl();
		var userAgent = "unknown";
		var ip = "unknown";
		var requestBody = "empty";

		if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var realIp))
		{
			ip = realIp.FirstOrDefault()!;
		}

		if (httpContext.Request.Headers.TryGetValue("User-Agent", out var realUserAgent))
		{
			userAgent = realUserAgent.FirstOrDefault()!;
		}

		if (httpContext.Request.Body.CanRead)
		{
			httpContext.Request.Body.Position = 0;
			
			using var reader = new StreamReader(httpContext.Request.Body);
			requestBody = await reader.ReadToEndAsync();
			
			if (httpContext.Request.Body.CanSeek)
			{
				httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
			}
		}
		
		var requestLog = new RequestLog
		{
			Timestamp = DateTime.Now,
			Ip = ip,
			Url = url,
			UserAgent = userAgent,
			RequestString = requestBody
		};
		
		return requestLog;
	}

	private async Task SendLogRequest(RequestLog log, ILogGateway logGateway)
	{
		try
		{
			await logGateway.LogRequest(log);
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error while writing request log: {ex.Message}");
		}
	}
}