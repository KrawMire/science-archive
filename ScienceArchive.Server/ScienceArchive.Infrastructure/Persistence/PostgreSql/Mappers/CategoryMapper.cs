using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Mappers;

internal class CategoryMapper : IInfrastructureMapper<Category, CategoryModel>
{
	public CategoryModel MapToModel(Category entity)
	{
		var subcategories = entity.Subcategories
			.Select(s => new SubcategoryModel
			{
				Id = s.Id.Value,
				Name = s.Name,
				Description = s.Description
			}).ToList();

		return new CategoryModel
		{
			Id = entity.Id.Value,
			Name = entity.Name,
			Subcategories = subcategories
		};
	}

	public Category MapToEntity(CategoryModel model)
	{
		var categoryId = CategoryId.CreateFromGuid(model.Id);
		var subcategories = model.Subcategories
			.Select(s => new Subcategory(SubcategoryId.CreateFromGuid(s.Id))
			{
				Name = s.Name,
				Description = s.Description
			}).ToList();

		return new Category(categoryId)
		{
			Name = model.Name,
			Description = model.Description,
			Subcategories = subcategories
		};
	}
}