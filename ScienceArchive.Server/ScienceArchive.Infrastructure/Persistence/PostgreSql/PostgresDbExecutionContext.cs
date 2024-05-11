using Microsoft.EntityFrameworkCore.Storage;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

public class PostgresDbExecutionContext
{
    public IDbContextTransaction? Transaction { get; set; }
}