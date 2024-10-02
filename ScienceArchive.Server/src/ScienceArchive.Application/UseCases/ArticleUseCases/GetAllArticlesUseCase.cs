using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetAllArticlesUseCase : IUseCase<GetAllArticlesRequestDto, GetAllArticlesResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetAllArticlesUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<GetAllArticlesResponseDto> Handle(GetAllArticlesRequestDto request, CancellationToken cancellationToken)
    {
        var articles = await _dbUnitOfWork.ArticleRepository.GetAll();
        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();

        return new GetAllArticlesResponseDto(articlesDtos);
    }
}