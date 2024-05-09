using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Application.Mappers;

internal class CategoryMapper : IApplicationMapper<Category, CategoryDto>
{
	public CategoryDto MapToDto(Category entity)
	{
		var subcategories = entity.Subcategories
			.Select(s => new CategoryDto
			{
				Id = s.Id.ToString(),
				Name = s.Name,
				Description = s.Description,
				Subcategories = null
			})
			.ToList();

		return new CategoryDto
		{
			Id = entity.Id.ToString(),
			Name = entity.Name,
			Description = entity.Description,
			Subcategories = subcategories
		};
	}

	public Category MapToEntity(CategoryDto dto)
	{
		var categoryId = string.IsNullOrWhiteSpace(dto.Id)
			? null
			: CategoryId.CreateNew();
		
		var subcategories = dto.Subcategories?
			.Select(s => new Subcategory(SubcategoryId.CreateFromString(s.Id))
			{
				Name = s.Name,
				Description = s.Description
			})
			.ToList() ?? new List<Subcategory>();

		return new Category(categoryId)
		{
			Name = dto.Name,
			Description = dto.Description,
			Subcategories = subcategories
		};
	}
}