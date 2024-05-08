using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class DeleteUserUseCase : IUseCase<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public DeleteUserUseCase(IUserRepository userRepository, IApplicationMapper<User, UserDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<DeleteUserResponseDto> Execute(DeleteUserRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var deletedUserId = await _userRepository.Delete(userId);

        return new DeleteUserResponseDto(deletedUserId.ToString());
    }
}