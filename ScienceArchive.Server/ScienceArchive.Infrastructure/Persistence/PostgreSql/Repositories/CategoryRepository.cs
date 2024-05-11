using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresCategoryRepository : ICategoryRepository
{
	private readonly PostgresDbContext _dbContext;
	
	public PostgresCategoryRepository(PostgresDbContext dbContext)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	}
	
	public Task<Category?> GetById(CategoryId id)
	{
		throw new NotImplementedException();
	}

	public Task<List<Category>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<Category> Create(Category newValue)
	{
		throw new NotImplementedException();
	}

	public Task<Category> Update(CategoryId id, Category newValue)
	{
		throw new NotImplementedException();
	}

	public Task<CategoryId> Delete(CategoryId id)
	{
		throw new NotImplementedException();
	}

	public Task<Subcategory?> GetSubcategoryById(CategoryId subcategoryId)
	{
		throw new NotImplementedException();
	}

	public Task<Category> CreateSubcategory(CategoryId categoryId, Category subcategory)
	{
		throw new NotImplementedException();
	}

	public Task<Category> UpdateSubcategory(CategoryId subcategoryId, Category subcategory)
	{
		throw new NotImplementedException();
	}

	public Task<CategoryId> DeleteSubcategory(CategoryId subcategoryId)
	{
		throw new NotImplementedException();
	}
}