using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Application service for managing user related operations.
/// </summary>
internal class UserApplicationService : BaseApplicationService, IUserApplicationService
{
    public UserApplicationService(IServiceProvider serviceProvider, IDbContext dbContext, IEventBus eventBus) 
        : base(serviceProvider, dbContext, eventBus) { }

    /// <inheritdoc/>
    public Task<GetAllUsersResponseDto> GetAllUsers(GetAllUsersRequestDto dto)
    {
        return ExecuteUseCase<GetAllUsersRequestDto, GetAllUsersResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetAllAuthorsResponseDto> GetAllAuthors(GetAllAuthorsRequestDto dto)
    {
        return ExecuteUseCase<GetAllAuthorsRequestDto, GetAllAuthorsResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<GetUserByIdResponseDto> GetUserById(GetUserByIdRequestDto dto)
    {
        return ExecuteUseCase<GetUserByIdRequestDto, GetUserByIdResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<DeleteUserResponseDto> DeleteUser(DeleteUserRequestDto dto)
    {
        return ExecuteUseCase<DeleteUserRequestDto, DeleteUserResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<UpdateUserResponseDto> UpdateUser(UpdateUserRequestDto dto)
    {
        return ExecuteUseCase<UpdateUserRequestDto, UpdateUserResponseDto>(dto);
    }
}