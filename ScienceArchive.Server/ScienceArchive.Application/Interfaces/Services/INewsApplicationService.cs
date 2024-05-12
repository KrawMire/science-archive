using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;

namespace ScienceArchive.Application.Interfaces.Services;

/// <summary>
/// Application news service
/// </summary>
public interface INewsApplicationService
{
    /// <summary>
    /// Get all news
    /// </summary>
    /// <param name="dto">DTO contract to get all news</param>
    /// <returns>Response DTO</returns>
    Task<GetAllNewsResponseDto> GetAllNews(GetAllNewsRequestDto dto);

    /// <summary>
    /// Get news by ID
    /// </summary>
    /// <param name="dto">DTO contract to get news by its ID</param>
    /// <returns>Response DTO</returns>
    Task<GetNewsByIdResponseDto> GetNewsById(GetNewsByIdRequestDto dto);

    /// <summary>
    /// Create news
    /// </summary>
    /// <param name="dto">DTO contract to create news</param>
    /// <returns>Response DTO</returns>
    Task<CreateNewsResponseDto> CreateNews(CreateNewsRequestDto dto);

    /// <summary>
    /// Update existing news
    /// </summary>
    /// <param name="dto">DTO contract to update news</param>
    /// <returns>Response DTO</returns>
    Task<UpdateNewsResponseDto> UpdateNews(UpdateNewsRequestDto dto);

    /// <summary>
    /// Delete existing news
    /// </summary>
    /// <param name="dto">DTO contract to delete news</param>
    /// <returns>Response DTO</returns>
    Task<DeleteNewsResponseDto> DeleteNews(DeleteNewsRequestDto dto);
}