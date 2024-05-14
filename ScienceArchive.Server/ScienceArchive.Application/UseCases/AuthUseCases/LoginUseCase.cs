using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Events.EventWrappers;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class LoginUseCase : IUseCase<LoginRequestDto, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    private readonly IEventBus _eventBus;
    
    public LoginUseCase(
        IAuthService authService, 
        IApplicationMapper<User, UserDto> userMapper, 
        IEventBus eventBus)
    {
        _authService = authService;
        _userMapper = userMapper;
        _eventBus = eventBus;
    }
    
    public async Task<LoginResponseDto> Handle(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var login = request.Login.Trim();
        var password = request.Password;
        
        var (user, code) = await _authService.AuthorizeUser(login, password);

        if (!user.IsConfirmed && code is not null)
        {
            await _eventBus.AddEventAsync(new UserRegisteredEventWrapper(
                new UserRegisteredEvent
                {
                    UserId = user.Id,
                    Name = user.Name,
                    ConfirmationCode = code,
                    Email = user.Email
                }));
        }
        
        return new LoginResponseDto(_userMapper.MapToDto(user));
    }
}