using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Mappers;

internal class SubcategoryMapper : IInfrastructureMapper<Subcategory, SubcategoryModel>
{
    public SubcategoryModel MapToModel(Subcategory entity)
    {
        var model = new SubcategoryModel
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            Description = entity.Description
        };
    
        return model;
    }

    public Subcategory MapToEntity(SubcategoryModel model)
    {
        var subcategoryId = SubcategoryId.CreateFromGuid(model.Id);

        return new Subcategory(subcategoryId)
        {
            Name = model.Name,
            Description = model.Description
        };
    }
}