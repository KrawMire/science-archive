using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class CreateArticleUseCase : IUseCase<CreateArticleRequestDto, CreateArticleResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IArticleRepository _articleRepository;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;

    public CreateArticleUseCase(IArticleRepository articleRepository, IApplicationMapper<Article, ArticleDto> articleMapper, IDbContext dbContext)
    {
        _articleRepository = articleRepository;
        _articleMapper = articleMapper;
        _dbContext = dbContext;
    }
    
    public async Task<CreateArticleResponseDto> Execute(CreateArticleRequestDto contract)
    {
        var article = _articleMapper.MapToEntity(contract.Article);
        article.SetToVerify();
        
        var createdArticle = await _articleRepository.Create(article);

        return new CreateArticleResponseDto(_articleMapper.MapToDto(createdArticle));
    }
}