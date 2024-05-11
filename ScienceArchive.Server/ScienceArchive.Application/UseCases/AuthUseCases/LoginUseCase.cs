using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class LoginUseCase : IUseCase<LoginRequestDto, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;

    public LoginUseCase(IAuthService authService, IApplicationMapper<User, UserDto> userMapper)
    {
        _authService = authService;
        _userMapper = userMapper;
    }
    
    public async Task<LoginResponseDto> Execute(LoginRequestDto contract)
    {
        var login = contract.Login.Trim();
        var password = contract.Password;
        
        var user = await _authService.AuthorizeUser(login, password);
        return new LoginResponseDto(_userMapper.MapToDto(user));
    }
}