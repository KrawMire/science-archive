using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetUserByIdUseCase : IUseCase<GetUserByIdRequestDto, GetUserByIdResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetUserByIdUseCase(IUserRepository userRepository, IApplicationMapper<User, UserDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<GetUserByIdResponseDto> Execute(GetUserByIdRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var user = await _userRepository.GetById(userId);

        var userDto = user is not null
            ? _userMapper.MapToDto(user)
            : null;

        return new GetUserByIdResponseDto(userDto);
    }
}