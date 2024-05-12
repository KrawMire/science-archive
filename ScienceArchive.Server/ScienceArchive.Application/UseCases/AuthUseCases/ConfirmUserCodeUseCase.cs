using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class ConfirmUserCodeUseCase : IUseCase<ConfirmUserCodeRequestDto, ConfirmUserCodeResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IApplicationMapper<User, UserDto> _userMapper;

    public ConfirmUserCodeUseCase(IAuthService authService, IApplicationMapper<User, UserDto> userMapper)
    {
        _authService = authService;
        _userMapper = userMapper;
    }

    public async Task<ConfirmUserCodeResponseDto> Execute(ConfirmUserCodeRequestDto contract)
    {
        var user = await _authService.ConfirmUser(UserId.CreateFromString(contract.UserId), contract.ConfirmCode);
        return new ConfirmUserCodeResponseDto(_userMapper.MapToDto(user));
    }
}