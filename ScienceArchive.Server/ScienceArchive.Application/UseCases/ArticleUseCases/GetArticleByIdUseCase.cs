using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticleByIdUseCase : IUseCase<GetArticleByIdRequestDto, GetArticleByIdResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticleByIdUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetArticleByIdResponseDto> Handle(GetArticleByIdRequestDto request, CancellationToken token)
    {
        var articleId = ArticleId.CreateFromString(request.Id);
        var article = await _dbUnitOfWork.ArticleRepository.GetById(articleId);
        
        var articleDto = article is not null
            ? _articleMapper.MapToDto(article)
            : throw new EntityNotFoundException(nameof(Article));
        
        return new GetArticleByIdResponseDto(articleDto);
    }
}