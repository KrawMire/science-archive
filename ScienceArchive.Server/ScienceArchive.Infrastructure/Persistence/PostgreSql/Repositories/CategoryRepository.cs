using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresCategoryRepository : ICategoryRepository
{
	private readonly PostgresExecutionContext _dbContext;
	private readonly IInfrastructureMapper<Category, CategoryModel> _mapper;
	private readonly IInfrastructureMapper<Subcategory, SubcategoryModel> _subcategoryMapper;
	
	public PostgresCategoryRepository(
		IInfrastructureMapper<Category, CategoryModel> mapper,
		IInfrastructureMapper<Subcategory, SubcategoryModel> subcategoryMapper, 
		PostgresExecutionContext dbContext)
	{
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_subcategoryMapper = subcategoryMapper ?? throw new ArgumentNullException(nameof(subcategoryMapper));
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