using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticlesByCategoryIdUseCase : IUseCase<GetArticlesByCategoryIdRequestDto, GetArticlesByCategoryIdResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    private readonly IApplicationMapper<Category, CategoryDto> _categoryMapper;
    
    public GetArticlesByCategoryIdUseCase(
        IArticleRepository articleRepository, 
        ICategoryRepository categoryRepository, 
        IApplicationMapper<Article, ArticleDto> articleMapper,
        IApplicationMapper<Category, CategoryDto> categoryMapper)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _articleMapper = articleMapper;
        _categoryMapper = categoryMapper;
    }
    
    public async Task<GetArticlesByCategoryIdResponseDto> Execute(GetArticlesByCategoryIdRequestDto contract)
    {
        var categoryId = CategoryId.CreateFromString(contract.CategoryId);
        var articles = await _articleRepository.GetVerifiedByCategoryId(categoryId);
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        
        var category = await _categoryRepository.GetSubcategoryById(categoryId);

        if (category is null)
        {
            throw new Exception("Category with specified ID was not found");
        }

        var categoryDto = _categoryMapper.MapToDto(category);
        
        return new GetArticlesByCategoryIdResponseDto(articlesDtos, categoryDto);
    }
}