using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class CreateArticleUseCase : IUseCase<CreateArticleRequestDto, CreateArticleResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;

    public CreateArticleUseCase(IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<CreateArticleResponseDto> Execute(CreateArticleRequestDto contract)
    {
        var article = _articleMapper.MapToEntity(contract.Article);
        article.SetToVerify();
        
        var createdArticle = await _dbContext.ArticleRepository.Create(article);

        return new CreateArticleResponseDto(_articleMapper.MapToDto(createdArticle));
    }
}