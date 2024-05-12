using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Events.EventWrappers;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class RegisterUseCase : IUseCase<RegisterRequestDto, RegisterResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    private readonly IEventBus _eventBus;
    
    public RegisterUseCase(
        IApplicationMapper<User, UserDto> userMapper, 
        IAuthService authService, 
        IEventBus eventBus)
    {
        _userMapper = userMapper;
        _authService = authService;
        _eventBus = eventBus;
    }
    
    public async Task<RegisterResponseDto> Handle(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var user = _userMapper.MapToEntity(request.User);
        var (createdUser, confirmCode) = await _authService.RegisterUser(user, request.Password);

        await _eventBus.AddEventAsync(new UserRegisteredEventWrapper(
            new UserRegisteredEvent
        {
            UserId = createdUser.Id,
            Name = createdUser.Name,
            ConfirmationCode = confirmCode,
            Email = createdUser.Email
        }));
        
        return new RegisterResponseDto(_userMapper.MapToDto(createdUser));
    }
}