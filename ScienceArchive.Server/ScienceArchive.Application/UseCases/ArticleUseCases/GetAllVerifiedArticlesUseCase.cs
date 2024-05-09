using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetAllVerifiedArticlesUseCase : IUseCase<GetAllVerifiedArticlesRequestDto, GetAllVerifiedArticlesResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetAllVerifiedArticlesUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<GetAllVerifiedArticlesResponseDto> Execute(GetAllVerifiedArticlesRequestDto contract)
    {
        var articles = await _articleRepository.GetAllVerified();
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();

        return new GetAllVerifiedArticlesResponseDto(articlesDtos);
    }
}