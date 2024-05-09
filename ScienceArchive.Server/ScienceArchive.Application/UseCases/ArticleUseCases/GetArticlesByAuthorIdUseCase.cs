using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticlesByAuthorIdUseCase : IUseCase<GetArticlesByAuthorIdRequestDto, GetArticlesByAuthorIdResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticlesByAuthorIdUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetArticlesByAuthorIdResponseDto> Execute(GetArticlesByAuthorIdRequestDto contract)
    {
        var authorId = UserId.CreateFromString(contract.AuthorId);
        var articles = await _dbContext.ArticleRepository.GetByAuthorId(authorId);

        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        return new GetArticlesByAuthorIdResponseDto(articlesDtos);
    }
}