using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresRoleRepository : IRoleRepository
{
    private readonly PostgresDbContext _dbContext;

    public PostgresRoleRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc/>
    public Task<List<Role>> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<Role?> GetById(RoleId id)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc/>
    public Task<List<RoleClaim>> GetUserClaims(UserId userId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<Role> Create(Role newValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<RoleId> Delete(RoleId id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<Role> Update(RoleId id, Role newValue)
    {
        throw new NotImplementedException();
    }
}