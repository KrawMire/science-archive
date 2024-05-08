using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class CreateArticleUseCase : IUseCase<CreateArticleRequestDto, CreateArticleResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;

    public CreateArticleUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<CreateArticleResponseDto> Execute(CreateArticleRequestDto contract)
    {
        var article = _articleMapper.MapToEntity(contract.Article);
        article.SetToVerify();
        
        var createdArticle = await _articleRepository.Create(article);

        return new CreateArticleResponseDto(_articleMapper.MapToDto(createdArticle));
    }
}