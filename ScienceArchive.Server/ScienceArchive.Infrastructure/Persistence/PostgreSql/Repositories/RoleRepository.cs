using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Exceptions;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresRoleRepository : IRoleRepository
{
    private readonly PostgresDbContext _dbContext;

    public PostgresRoleRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc/>
    public async Task<List<Role>> GetAll()
    {
        var roles = await _dbContext
            .Roles
            .Include(r => r.Claims)
            .ToListAsync();

        return roles
            .Select(r => new Role(RoleId.CreateFromGuid(r.Id))
            {
                Name = r.Name,
                Description = r.Description,
                Claims = r.Claims.Select(c => new RoleClaim
                {
                    Value = c.Value,
                    Description = c.Description
                }).ToList()
            }).ToList();
    }

    /// <inheritdoc/>
    public async Task<Role?> GetById(RoleId id)
    {
        var role = await _dbContext
            .Roles
            .Where(r => r.Id == id.Value)
            .Include(r => r.Claims)
            .FirstOrDefaultAsync();

        if (role is null)
        {
            return null;
        }

        return new Role(RoleId.CreateFromGuid(role.Id))
        {
            Name = role.Name,
            Description = role.Description,
            Claims = role.Claims.Select(c => new RoleClaim
            {
                Value = c.Value,
                Description = c.Description
            }).ToList()
        };
    }
    
    /// <inheritdoc/>
    public async Task<List<RoleClaim>> GetUserClaims(UserId userId)
    {
        var user = await _dbContext
            .Users
            .Where(u => u.Id == userId.Value)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(userId));
        }
        
        var claims = await _dbContext
            .Roles
            .Where(r => r.Id == userId.Value)
            .SelectMany(r => r.Claims)
            .Distinct()
            .ToListAsync();

        return claims.Select(c => new RoleClaim
        {
            Value = c.Value,
            Description = c.Description
        }).ToList();
    }

    /// <inheritdoc/>
    public async Task<Role> Create(Role newValue)
    {
        var claims = await _dbContext
            .Claims
            .Where(c => newValue.Claims.Any(rc => rc.Value == c.Value))
            .ToListAsync();
        
        var role = await _dbContext
            .Roles
            .AddAsync(new Entities.Role
            {
                Id = newValue.Id.Value,
                Name = newValue.Name,
                Description = newValue.Description ?? "No description",
                Claims = claims
            });

        await _dbContext.SaveChangesAsync();
        
        var createdRole = await GetById(RoleId.CreateFromGuid(role.Entity.Id));

        if (createdRole is null)
        {
            throw new PersistenceException("Role was not created");
        }
        
        return createdRole;
    }
    
    /// <inheritdoc/>
    public async Task<Role> Update(RoleId id, Role newValue)
    {
        var role = await _dbContext.Roles
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();

        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Role));
        }

        var claims = await _dbContext
            .Claims
            .Where(c => newValue.Claims.Any(rc => rc.Value == c.Value))
            .ToListAsync();
        
        role.Name = newValue.Name;
        role.Description = newValue.Description ?? "No description";
        role.Claims = claims;

        await _dbContext.SaveChangesAsync();

        return (await GetById(id))!;
    }

    /// <inheritdoc/>
    public async Task<RoleId> Delete(RoleId id)
    {
        var role = await _dbContext.Roles
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();
        
        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Role));
        }
        
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();
        
        return id;
    }
}