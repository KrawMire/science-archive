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
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public UpdateArticleUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<UpdateArticleResponseDto> Execute(UpdateArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.Id);
        var article = _articleMapper.MapToEntity(contract.Article);
        article.SetToVerify();
        
        var updatedArticle = await _dbContext.ArticleRepository.Update(articleId, article);
        return new UpdateArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}