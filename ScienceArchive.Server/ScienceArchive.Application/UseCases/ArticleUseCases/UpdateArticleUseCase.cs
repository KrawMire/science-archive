using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class UpdateArticleUseCase : IUseCase<UpdateArticleRequestDto, UpdateArticleResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public UpdateArticleUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<UpdateArticleResponseDto> Execute(UpdateArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.Id);
        var article = _articleMapper.MapToEntity(contract.Article);
        article.SetToVerify();
        
        var updatedArticle = await _articleRepository.Update(articleId, article);
        return new UpdateArticleResponseDto(_articleMapper.MapToDto(updatedArticle));
    }
}