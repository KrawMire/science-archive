using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetAllUsersUseCase : IUseCase<GetAllUsersRequestDto, GetAllUsersResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetAllUsersUseCase(IUserRepository userRepository, IApplicationMapper<User, UserDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<GetAllUsersResponseDto> Execute(GetAllUsersRequestDto contract)
    {
        var users = await _userRepository.GetAll();
        var usersDtos = users.Select(user => _userMapper.MapToDto(user)).ToList();
        
        return new GetAllUsersResponseDto(usersDtos);
    }
}