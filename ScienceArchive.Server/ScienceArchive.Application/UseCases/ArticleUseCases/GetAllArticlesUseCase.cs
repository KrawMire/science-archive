using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetAllArticlesUseCase : IUseCase<GetAllArticlesRequestDto, GetAllArticlesResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetAllArticlesUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllArticlesResponseDto> Execute(GetAllArticlesRequestDto contract)
    {
        var articles = await _dbContext.ArticleRepository.GetAll();
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();

        return new GetAllArticlesResponseDto(articlesDtos);
    }
}