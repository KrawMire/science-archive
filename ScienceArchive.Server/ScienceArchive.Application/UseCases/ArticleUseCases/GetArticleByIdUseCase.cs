using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Exceptions;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class GetArticleByIdUseCase : IUseCase<GetArticleByIdRequestDto, GetArticleByIdResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Article, ArticleDto> _articleMapper;
    
    public GetArticleByIdUseCase(
        IApplicationMapper<Article, ArticleDto> articleMapper, 
        IDbUnitOfWork dbUnitOfWork, 
        IAuthService authService)
    {
        _articleMapper = articleMapper;
        _dbUnitOfWork = dbUnitOfWork;
        _authService = authService;
    }
    
    public async Task<GetArticleByIdResponseDto> Handle(GetArticleByIdRequestDto request, CancellationToken token)
    {
        var articleId = ArticleId.CreateFromString(request.Id);
        var article = await _dbUnitOfWork.ArticleRepository.GetById(articleId);
        
        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        var articleDto = _articleMapper.MapToDto(article);

        if (article.Status == ArticleStatus.Verified)
        {
            return new GetArticleByIdResponseDto(articleDto);
        }
        
        var userId = UserId.CreateFromString(request.UserId ?? throw new EntityNotFoundException(nameof(User)));

        if (article.Authors.Any(a => a.UserId.Equals(userId)))
        {
            return new GetArticleByIdResponseDto(articleDto);
        }
        
        var requiredClaims = await _dbUnitOfWork.RoleRepository.GetClaimsByValues(request.RequiredClaims);

        if (requiredClaims.Count != request.RequiredClaims.Count)
        {
            throw new CannotFindAllClaimsException(
                request.RequiredClaims, 
                requiredClaims.Select(rc => rc.Value).ToList());
        }
        
        if (await _authService.UserHasClaims(userId, requiredClaims))
        {
            return new GetArticleByIdResponseDto(articleDto);
        }

        throw new UserCannotViewArticleException();
    }
}