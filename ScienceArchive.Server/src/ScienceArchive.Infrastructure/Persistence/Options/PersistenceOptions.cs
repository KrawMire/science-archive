using ScienceArchive.Infrastructure.Persistence.PostgreSql.Options;

namespace ScienceArchive.Infrastructure.Persistence.Options;

/// <summary>
/// Options of connection to different data sources
/// </summary>
public class PersistenceOptions
{
    /// <summary>
    /// Options for connecting to a PostgreSQL database.
    /// </summary>
    public required PostgresConnectionOptions PostgresConnectionOptions { get; init; }
}