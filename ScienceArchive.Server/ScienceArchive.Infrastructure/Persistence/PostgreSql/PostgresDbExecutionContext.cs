using Microsoft.EntityFrameworkCore.Storage;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

internal class PostgresDbExecutionContext
{
    public IDbContextTransaction? Transaction { get; set; }
}