using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class NewsApplicationService : BaseApplicationService, INewsApplicationService
{
    public NewsApplicationService(IServiceProvider serviceProvider) : base(serviceProvider) { }

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
        return ExecuteUseCase<CreateNewsRequestDto, CreateNewsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<UpdateNewsResponseDto> UpdateNews(UpdateNewsRequestDto dto)
    {
        return ExecuteUseCase<UpdateNewsRequestDto, UpdateNewsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<DeleteNewsResponseDto> DeleteNews(DeleteNewsRequestDto dto)
    {
        return ExecuteUseCase<DeleteNewsRequestDto, DeleteNewsResponseDto>(dto);
    }
}