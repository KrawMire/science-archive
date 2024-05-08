using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticleByIdUseCase : IUseCase<GetArticleByIdRequestDto, GetArticleByIdResponseDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticleByIdUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
    }
    
    public async Task<GetArticleByIdResponseDto> Execute(GetArticleByIdRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.Id);
        var article = await _articleRepository.GetById(articleId);
        
        var articleDto = article is not null
            ? _articleMapper.MapToDto(article)
            : throw new EntityNotFoundException(nameof(Article));
        
        return new GetArticleByIdResponseDto(articleDto);
    }
}