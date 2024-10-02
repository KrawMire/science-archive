using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetVerifiedArticlesByCategoryIdUseCase : IUseCase<GetVerifiedArticlesByCategoryIdRequestDto, GetVerifiedArticlesByCategoryIdResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    private readonly IApplicationMapper<Subcategory, CategoryDto> _categoryMapper;
    
    public GetVerifiedArticlesByCategoryIdUseCase(
        IApplicationMapper<Article, ArticleDto> articleMapper,
        IApplicationMapper<Subcategory, CategoryDto> categoryMapper, 
        IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _categoryMapper = categoryMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetVerifiedArticlesByCategoryIdResponseDto> Handle(GetVerifiedArticlesByCategoryIdRequestDto request, CancellationToken cancellationToken)
    {
        var categoryId = SubcategoryId.CreateFromString(request.CategoryId);
        var articles = await _dbUnitOfWork.ArticleRepository.GetVerifiedBySubcategoryId(categoryId);
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        
        var subcategory = await _dbUnitOfWork.CategoryRepository.GetSubcategoryById(categoryId);

        if (subcategory is null)
        {
            throw new Exception("Category with specified ID was not found");
        }

        var categoryDto = _categoryMapper.MapToDto(subcategory);
        
        return new GetVerifiedArticlesByCategoryIdResponseDto(articlesDtos, categoryDto);
    }
}