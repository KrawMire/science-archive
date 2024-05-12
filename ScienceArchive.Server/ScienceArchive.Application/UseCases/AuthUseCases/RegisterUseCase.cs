using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
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
    
    public async Task<RegisterResponseDto> Execute(RegisterRequestDto contract)
    {
        var user = _userMapper.MapToEntity(contract.User);
        var (createdUser, confirmCode) = await _authService.RegisterUser(user, contract.Password);

        await _eventBus.AddEventAsync(new UserRegisteredEvent
        {
            UserId = createdUser.Id,
            ConfirmationCode = confirmCode,
            Email = createdUser.Email
        });
        
        return new RegisterResponseDto(_userMapper.MapToDto(createdUser));
    }
}