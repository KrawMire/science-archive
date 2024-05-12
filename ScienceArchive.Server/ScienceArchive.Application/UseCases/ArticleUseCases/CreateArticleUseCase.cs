using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class CreateArticleUseCase : IUseCase<CreateArticleRequestDto, CreateArticleResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;

    public CreateArticleUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<CreateArticleResponseDto> Handle(CreateArticleRequestDto request, CancellationToken cancellationToken)
    {
        var article = _articleMapper.MapToEntity(request.Article);
        article.SetToVerify();
        
        var createdArticle = await _dbUnitOfWork.ArticleRepository.Create(article);

        return new CreateArticleResponseDto(_articleMapper.MapToDto(createdArticle));
    }
}