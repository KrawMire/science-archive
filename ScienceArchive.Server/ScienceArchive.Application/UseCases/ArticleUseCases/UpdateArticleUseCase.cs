using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class UpdateArticleUseCase : IUseCase<UpdateArticleRequestDto, UpdateArticleResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public UpdateArticleUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<UpdateArticleResponseDto> Handle(UpdateArticleRequestDto request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.CreateFromString(request.Id);
        var article = _articleMapper.MapToEntity(request.Article);
        article.SetToVerify();
        
        var updatedArticle = await _dbUnitOfWork.ArticleRepository.Update(articleId, article);
        return new UpdateArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}