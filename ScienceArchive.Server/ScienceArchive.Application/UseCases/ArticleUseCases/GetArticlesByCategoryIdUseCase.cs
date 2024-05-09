using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticlesByCategoryIdUseCase : IUseCase<GetArticlesByCategoryIdRequestDto, GetArticlesByCategoryIdResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    private readonly IApplicationMapper<Subcategory, CategoryDto> _categoryMapper;
    
    public GetArticlesByCategoryIdUseCase(
        IApplicationMapper<Article, ArticleDto> articleMapper,
        IApplicationMapper<Subcategory, CategoryDto> categoryMapper, 
        IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _categoryMapper = categoryMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetArticlesByCategoryIdResponseDto> Execute(GetArticlesByCategoryIdRequestDto contract)
    {
        var categoryId = CategoryId.CreateFromString(contract.CategoryId);
        var articles = await _dbContext.ArticleRepository.GetVerifiedByCategoryId(categoryId);
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        
        var subcategory = await _dbContext.CategoryRepository.GetSubcategoryById(categoryId);

        if (subcategory is null)
        {
            throw new Exception("Category with specified ID was not found");
        }

        var categoryDto = _categoryMapper.MapToDto(subcategory);
        
        return new GetArticlesByCategoryIdResponseDto(articlesDtos, categoryDto);
    }
}