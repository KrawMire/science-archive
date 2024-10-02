namespace ScienceArchive.Shared.Abstractions.Logging.Models;

public record RequestLog
{
	public DateTime Timestamp { get; set; }
	public required string Ip { get; set; }
	public required string Url { get; set; }
	public required string UserAgent { get; set; }
	public string? RequestString { get; set; }
	public string? ResponseString { get; set; }
}