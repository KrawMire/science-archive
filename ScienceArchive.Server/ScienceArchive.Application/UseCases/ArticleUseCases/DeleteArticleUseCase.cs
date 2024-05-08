using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class DeleteArticleUseCase : IUseCase<DeleteArticleRequestDto, DeleteArticleResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public DeleteArticleUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<DeleteArticleResponseDto> Execute(DeleteArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.Id);
        var deletedArticleId = await _articleRepository.Delete(articleId);
        
        return new DeleteArticleResponseDto(deletedArticleId.ToString());
    }
}