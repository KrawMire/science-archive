using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Application.Mappers;

internal class SubcategoryMapper : IApplicationMapper<Subcategory, CategoryDto>
{
    public CategoryDto MapToDto(Subcategory entity)
    {
        return new CategoryDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description
        };
    }

    public Subcategory MapToEntity(CategoryDto dto)
    {
        var categoryId = SubcategoryId.CreateFromString(dto.Id);
		
        var subcategories = dto.Subcategories?
            .Select(s => new Subcategory(SubcategoryId.CreateFromString(s.Id))
            {
                Name = s.Name,
                Description = s.Description
            })
            .ToList() ?? new List<Subcategory>();

        return new Subcategory(categoryId)
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }
}