using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticlesByAuthorIdUseCase : IUseCase<GetArticlesByAuthorIdRequestDto, GetArticlesByAuthorIdResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticlesByAuthorIdUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetArticlesByAuthorIdResponseDto> Handle(GetArticlesByAuthorIdRequestDto request, CancellationToken cancellationToken)
    {
        var authorId = UserId.CreateFromString(request.AuthorId);
        var articles = await _dbUnitOfWork.ArticleRepository.GetByAuthorId(authorId);

        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        return new GetArticlesByAuthorIdResponseDto(articlesDtos);
    }
}