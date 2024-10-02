namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Options;

/// <summary>
/// Represents the options for connecting to a Postgres database.
/// </summary>
public class PostgresConnectionOptions
{
    /// <summary>
    /// Represents the connection string for connecting to a Postgres database.
    /// </summary>
    public required string PostgresConnectionString { get; set; }
}