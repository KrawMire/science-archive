using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresRoleRepository : IRoleRepository
{
    private readonly PostgresExecutionContext _dbContext;
    private readonly IInfrastructureMapper<Role, RoleModel> _roleMapper;
    private readonly IInfrastructureMapper<RoleClaim, RoleClaimModel> _claimMapper;

    public PostgresRoleRepository(
        IInfrastructureMapper<Role, RoleModel> roleMapper,
        IInfrastructureMapper<RoleClaim, RoleClaimModel> claimMapper, 
        PostgresExecutionContext dbContext)
    {
        _claimMapper = claimMapper ?? throw new ArgumentNullException(nameof(claimMapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _roleMapper = roleMapper ?? throw new ArgumentNullException(nameof(roleMapper));
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