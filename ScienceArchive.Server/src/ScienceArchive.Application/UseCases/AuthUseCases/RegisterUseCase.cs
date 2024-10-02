using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class RegisterUseCase : IUseCase<RegisterRequestDto, RegisterResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public RegisterUseCase(
        IApplicationMapper<User, UserDto> userMapper, 
        IAuthService authService)
    {
        _userMapper = userMapper;
        _authService = authService;
    }
    
    public async Task<RegisterResponseDto> Handle(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var user = _userMapper.MapToEntity(request.User);
        var (createdUser, _) = await _authService.RegisterUser(user, request.Password);
        
        return new RegisterResponseDto(_userMapper.MapToDto(createdUser));
    }
}