namespace ScienceArchive.Application.Abstractions.Monitoring.Models;

/// <summary>
/// System status
/// </summary>
public class SystemStatus
{
    /// <summary>
    /// Is service working
    /// </summary>
    public required bool IsWorking { get; set; }
}