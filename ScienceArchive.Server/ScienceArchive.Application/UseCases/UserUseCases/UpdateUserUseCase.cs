using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class UpdateUserUseCase : IUseCase<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public UpdateUserUseCase(IUserRepository userRepository, IApplicationMapper<User, UserDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<UpdateUserResponseDto> Execute(UpdateUserRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var user = _userMapper.MapToEntity(contract.User);
        var updatedUser = await _userRepository.Update(userId, user);

        return new UpdateUserResponseDto(_userMapper.MapToDto(updatedUser));
    }
}