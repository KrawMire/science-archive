using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Dtos.Category.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;

namespace ScienceArchive.Application.UseCases.CategoryUseCases;

internal class GetAllCategoriesUseCase : IUseCase<GetAllCategoriesRequestDto, GetAllCategoriesResponseDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IApplicationMapper<Category, CategoryDto> _categoryMapper;
    
    public GetAllCategoriesUseCase(ICategoryRepository categoryRepository, IApplicationMapper<Category, CategoryDto> categoryMapper)
    {
        _categoryRepository = categoryRepository;
        _categoryMapper = categoryMapper;
    }
    
    public async Task<GetAllCategoriesResponseDto> Execute(GetAllCategoriesRequestDto contract)
    {
        var categories = await _categoryRepository.GetAll();
        var categoriesDtos = categories
            .Select(_categoryMapper.MapToDto)
            .ToList();

        return new GetAllCategoriesResponseDto(categoriesDtos);
    }
}