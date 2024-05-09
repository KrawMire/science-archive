using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class ApproveArticleUseCase : IUseCase<ApproveArticleRequestDto, ApproveArticleResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public ApproveArticleUseCase(IDbContext dbContext, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _dbContext = dbContext;
        _articleMapper = articleMapper;
    }
    
    public async Task<ApproveArticleResponseDto> Execute(ApproveArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.ArticleId);
        var article = await _dbContext.ArticleRepository.GetById(articleId);

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        article.Approve();

        var updatedArticle = await _dbContext.ArticleRepository.Update(articleId, article);
        return new ApproveArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}