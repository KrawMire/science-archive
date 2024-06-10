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
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public DeclineArticleUseCase(
        IDbUnitOfWork dbUnitOfWork, 
        IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<DeclineArticleResponseDto> Handle(DeclineArticleRequestDto request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.CreateFromString(request.ArticleId);
        var article = await _dbUnitOfWork.ArticleRepository.GetById(articleId);

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        article.Decline();

        var updatedArticle = await _dbUnitOfWork.ArticleRepository.Update(articleId, article);
        
        return new DeclineArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}