using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class DeclineArticleUseCase : IUseCase<DeclineArticleRequestDto, DeclineArticleResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public DeclineArticleUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<DeclineArticleResponseDto> Execute(DeclineArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.ArticleId);
        var article = await _dbContext.ArticleRepository.GetById(articleId);

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        article.Decline();

        var updatedArticle = await _dbContext.ArticleRepository.Update(articleId, article);
        return new DeclineArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}