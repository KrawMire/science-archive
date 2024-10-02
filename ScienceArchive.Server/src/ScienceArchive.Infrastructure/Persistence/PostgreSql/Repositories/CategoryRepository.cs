using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Exceptions;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresCategoryRepository : ICategoryRepository
{
	private readonly PostgresDbContext _dbContext;
	private readonly IDomainEventBus _eventBus;
	
	public PostgresCategoryRepository(PostgresDbContext dbContext, IDomainEventBus eventBus)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		_eventBus = eventBus;
	}
	
	public async Task<Category?> GetById(CategoryId id)
	{
		var category = await _dbContext
			.Categories
			.Where(c => c.Id == id.Value)
			.Include(c => c.Subcategories)
			.FirstOrDefaultAsync();
		
		if (category is null)
		{
			return null;
		}
		
		var domainCategory = new Category(CategoryId.CreateFromGuid(category.Id))
		{
			Name = category.Name,
			Description = category.Description,
			Subcategories = category
				.Subcategories
				.Select(s => new Subcategory(SubcategoryId.CreateFromGuid(s.Id))
				{
					Name = s.Name,
					Description = s.Description
				}).ToList()
		};
		
		_eventBus.AddTrackedEntity(domainCategory);

		return domainCategory;
	}

	public async Task<List<Category>> GetAll()
	{
		var categories = await _dbContext
			.Categories
			.Include(c => c.Subcategories)
			.ToListAsync();

		return categories.Select(c => new Category(CategoryId.CreateFromGuid(c.Id))
		{
			Name = c.Name,
			Description = c.Description,
			Subcategories = c
				.Subcategories
				.Select(s => new Subcategory(SubcategoryId.CreateFromGuid(s.Id))
				{
					Name = s.Name,
					Description = s.Description
				}).ToList()
		}).ToList();
	}

	public async Task<Category> Create(Category newValue)
	{
		var category = await _dbContext
			.Categories
			.AddAsync(new Entities.Category
			{
				Id = newValue.Id.Value,
				Name = newValue.Name,
				Description = newValue.Description
			});
		
		await _dbContext
			.Subcategories
			.AddRangeAsync(newValue.Subcategories.Select(s => new Entities.Subcategory
			{
				Id = s.Id.Value,
				CategoryId = newValue.Id.Value,
				Name = s.Name,
				Description = s.Description
			}));

		await _dbContext.SaveChangesAsync();

		var createdCategory = await GetById(CategoryId.CreateFromGuid(category.Entity.Id));

		if (createdCategory is null)
		{
			throw new PersistenceException("Category was not created");
		}
        
		return createdCategory;
	}

	public async Task<Category> Update(CategoryId id, Category newValue)
	{
		var category = await _dbContext.Categories
			.Where(c => c.Id == id.Value)
			.FirstOrDefaultAsync();

		if (category is null)
		{
			throw new EntityNotFoundException(nameof(Category));
		}
		
		category.Name = newValue.Name;
		category.Description = newValue.Description;

		await _dbContext.SaveChangesAsync();

		return (await GetById(id))!;
	}

	public Task<CategoryId> Delete(CategoryId id)
	{
		throw new NotImplementedException();
	}

	public async Task<Subcategory?> GetSubcategoryById(SubcategoryId subcategoryId)
	{
		var subcategory = await _dbContext.Subcategories
			.Where(s => s.Id == subcategoryId.Value)
			.FirstOrDefaultAsync();

		if (subcategory is null)
		{
			return null;
		}

		return new Subcategory(SubcategoryId.CreateFromGuid(subcategory.Id))
		{
			Name = subcategory.Name,
			Description = subcategory.Description
		};
	}
}