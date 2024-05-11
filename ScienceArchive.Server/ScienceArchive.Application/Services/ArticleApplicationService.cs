using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Represents a service for managing articles.
/// </summary>
internal class ArticleApplicationService : BaseApplicationService, IArticleApplicationService
{ 
    public ArticleApplicationService(IServiceProvider serviceProvider, IDbUnitOfWork dbUnitOfWork, IEventBus eventBus) 
        : base(serviceProvider, dbUnitOfWork, eventBus) { }

    /// <inheritdoc />
    public Task<GetAllArticlesResponseDto> GetAllArticles(GetAllArticlesRequestDto dto)
    {
        return ExecuteUseCase<GetAllArticlesRequestDto, GetAllArticlesResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetAllArticlesResponseDto> GetAllVerifiedArticles(GetAllVerifiedArticlesRequestDto dto)
    {
        return ExecuteUseCase<GetAllVerifiedArticlesRequestDto, GetAllArticlesResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetArticlesByAuthorIdResponseDto> GetArticlesByAuthorId(GetArticlesByAuthorIdRequestDto dto)
    {
        return ExecuteUseCase<GetArticlesByAuthorIdRequestDto, GetArticlesByAuthorIdResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetVerifiedArticlesByAuthorIdResponseDto> GetVerifiedArticlesByAuthorId(GetVerifiedArticlesByAuthorIdRequestDto dto)
    {
        return ExecuteUseCase<GetVerifiedArticlesByAuthorIdRequestDto, GetVerifiedArticlesByAuthorIdResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetArticleByIdResponseDto> GetArticleById(GetArticleByIdRequestDto dto)
    {
        return ExecuteUseCase<GetArticleByIdRequestDto, GetArticleByIdResponseDto>(dto);
    }
    
    /// <inheritdoc/>
    public Task<GetArticlesByCategoryIdResponseDto> GetArticlesByCategoryId(GetArticlesByCategoryIdRequestDto dto)
    {
        return ExecuteUseCase<GetArticlesByCategoryIdRequestDto, GetArticlesByCategoryIdResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<CreateArticleResponseDto> CreateArticle(CreateArticleRequestDto dto)
    {
        return ExecuteUseCase<CreateArticleRequestDto, CreateArticleResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<DeleteArticleResponseDto> DeleteArticle(DeleteArticleRequestDto dto)
    {
        return ExecuteUseCase<DeleteArticleRequestDto, DeleteArticleResponseDto>(dto);   
    }

    /// <inheritdoc/>
    public Task<ApproveArticleResponseDto> ApproveArticle(ApproveArticleRequestDto dto)
    {
        return ExecuteUseCase<ApproveArticleRequestDto, ApproveArticleResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<DeclineArticleResponseDto> DeclineArticle(DeclineArticleRequestDto dto)
    {
        return ExecuteUseCase<DeclineArticleRequestDto, DeclineArticleResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<UpdateArticleResponseDto> UpdateArticle(UpdateArticleRequestDto dto)
    {
        return ExecuteUseCase<UpdateArticleRequestDto, UpdateArticleResponseDto>(dto);
    }
}