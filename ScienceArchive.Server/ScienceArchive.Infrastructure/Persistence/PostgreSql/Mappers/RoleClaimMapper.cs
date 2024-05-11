using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Mappers;

internal class RoleClaimMapper : IInfrastructureMapper<RoleClaim, RoleClaimModel>
{
    public RoleClaimModel MapToModel(RoleClaim entity)
    {
        return new RoleClaimModel
        {
            Value = entity.Value,
            Description = entity.Description
        };
    }

    public RoleClaim MapToEntity(RoleClaimModel model)
    {
        return new RoleClaim
        {
            Value = model.Value,
            Description = model.Description
        };
    }
}