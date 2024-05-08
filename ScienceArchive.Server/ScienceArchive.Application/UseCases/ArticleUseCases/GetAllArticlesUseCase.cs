using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetAllArticlesUseCase : IUseCase<GetAllArticlesRequestDto, GetAllArticlesResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetAllArticlesUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<GetAllArticlesResponseDto> Execute(GetAllArticlesRequestDto contract)
    {
        var articles = await _articleRepository.GetAll();
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();

        return new GetAllArticlesResponseDto(articlesDtos);
    }
}