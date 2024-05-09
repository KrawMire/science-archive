using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Dtos.Category.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Category;

namespace ScienceArchive.Application.UseCases.CategoryUseCases;

internal class GetAllCategoriesUseCase : IUseCase<GetAllCategoriesRequestDto, GetAllCategoriesResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Category, CategoryDto> _categoryMapper;
    
    public GetAllCategoriesUseCase(IApplicationMapper<Category, CategoryDto> categoryMapper, IDbContext dbContext)
    {
        _categoryMapper = categoryMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllCategoriesResponseDto> Execute(GetAllCategoriesRequestDto contract)
    {
        var categories = await _dbContext.CategoryRepository.GetAll();
        var categoriesDtos = categories
            .Select(_categoryMapper.MapToDto)
            .ToList();

        return new GetAllCategoriesResponseDto(categoriesDtos);
    }
}