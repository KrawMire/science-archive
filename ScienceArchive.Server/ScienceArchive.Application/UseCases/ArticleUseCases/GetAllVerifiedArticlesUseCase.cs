using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetAllVerifiedArticlesUseCase : IUseCase<GetAllVerifiedArticlesRequestDto, GetAllVerifiedArticlesResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetAllVerifiedArticlesUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllVerifiedArticlesResponseDto> Execute(GetAllVerifiedArticlesRequestDto contract)
    {
        var articles = await _dbContext.ArticleRepository.GetAllVerified();
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();

        return new GetAllVerifiedArticlesResponseDto(articlesDtos);
    }
}