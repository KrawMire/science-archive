using System.Data;
using Dapper;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Exceptions;
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
	
	public async Task<Category?> GetById(CategoryId id)
	{
		var parameters = new DynamicParameters();
		parameters.Add("Id", id.Value);

		var category = await _dbContext.Connection.QueryFirstOrDefaultAsync<CategoryModel?>(
			"SELECT * FROM func_get_category_by_id(@Id::uuid)",
			parameters,
			commandType: CommandType.Text,
			transaction: _dbContext.Transaction);

		return category is null 
			? null 
			: _mapper.MapToEntity(category);
	}

	public async Task<List<Category>> GetAll()
	{
		var categories = await _dbContext.Connection.QueryAsync<CategoryModel>(
			"SELECT * FROM func_get_all_categories()",
			commandType: CommandType.Text,
			transaction: _dbContext.Transaction);

		if (categories is null)
		{
			throw new EntityNotFoundException(nameof(Category));
		}

		return categories.Select(c => _mapper.MapToEntity(c)).ToList();
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

	public async Task<Subcategory?> GetSubcategoryById(CategoryId subcategoryId)
	{
		var parameters = new DynamicParameters();
		parameters.Add("Id", subcategoryId.Value);

		var subcategory = await _dbContext.Connection.QueryFirstOrDefaultAsync<SubcategoryModel?>(
			"SELECT * FROM func_get_subcategory_by_id(@Id::uuid)",
			parameters,
			commandType: CommandType.Text,
			transaction: _dbContext.Transaction);

		return subcategory is null 
			? null 
			: _subcategoryMapper.MapToEntity(subcategory);
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