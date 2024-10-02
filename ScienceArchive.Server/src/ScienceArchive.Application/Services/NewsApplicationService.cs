using MediatR;
using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Provides methods to perform operations related to news articles.
/// </summary>
internal class NewsApplicationService : BaseApplicationService, INewsApplicationService
{
    public NewsApplicationService(IMediator mediator, IDbUnitOfWork dbUnitOfWork, IDomainEventBus eventBus) 
        : base(mediator, dbUnitOfWork, eventBus) { }

    /// <inheritdoc/>
    public Task<GetAllNewsResponseDto> GetAllNews(GetAllNewsRequestDto dto)
    {
        return ExecuteUseCase<GetAllNewsRequestDto, GetAllNewsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetNewsByIdResponseDto> GetNewsById(GetNewsByIdRequestDto dto)
    {
        return ExecuteUseCase<GetNewsByIdRequestDto, GetNewsByIdResponseDto>(dto);   
    }

    /// <inheritdoc/>
    public Task<CreateNewsResponseDto> CreateNews(CreateNewsRequestDto dto)
    {
        return ExecuteTransactionalUseCase<CreateNewsRequestDto, CreateNewsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<UpdateNewsResponseDto> UpdateNews(UpdateNewsRequestDto dto)
    {
        return ExecuteTransactionalUseCase<UpdateNewsRequestDto, UpdateNewsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<DeleteNewsResponseDto> DeleteNews(DeleteNewsRequestDto dto)
    {
        return ExecuteTransactionalUseCase<DeleteNewsRequestDto, DeleteNewsResponseDto>(dto);
    }
}