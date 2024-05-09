using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class RegisterUseCase : IUseCase<RegisterRequestDto, RegisterResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public RegisterUseCase(IApplicationMapper<User, UserDto> userMapper, IAuthService authService)
    {
        _userMapper = userMapper;
        _authService = authService;
    }
    
    public async Task<RegisterResponseDto> Execute(RegisterRequestDto contract)
    {
        var user = _userMapper.MapToEntity(contract.User);
        var createdUser = await _authService.RegisterUser(user, contract.Password);

        return new RegisterResponseDto(_userMapper.MapToDto(createdUser));
    }
}