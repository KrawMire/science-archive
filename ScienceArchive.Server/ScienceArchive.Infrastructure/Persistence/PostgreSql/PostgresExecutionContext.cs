using Npgsql;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Options;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

internal class PostgresExecutionContext
{
    public PostgresExecutionContext(PostgresConnectionOptions connectionOptions)
    {
        Connection = new NpgsqlConnection(connectionOptions.PostgresConnectionString);
    }
    
    public NpgsqlTransaction? Transaction { get; set; }
    public NpgsqlConnection Connection { get; set; }
}