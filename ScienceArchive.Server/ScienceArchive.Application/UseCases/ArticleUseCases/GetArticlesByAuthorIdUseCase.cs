using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticlesByAuthorIdUseCase : IUseCase<GetArticlesByAuthorIdRequestDto, GetArticlesByAuthorIdResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticlesByAuthorIdUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<GetArticlesByAuthorIdResponseDto> Execute(GetArticlesByAuthorIdRequestDto contract)
    {
        var authorId = UserId.CreateFromString(contract.AuthorId);
        var articles = await _articleRepository.GetByAuthorId(authorId);

        var articlesDtos = articles.Select(_articleMapper.MapToDto).ToList();
        return new GetArticlesByAuthorIdResponseDto(articlesDtos);
    }
}