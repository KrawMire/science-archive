using ScienceArchive.Application.Abstractions.Persistence;
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
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<User, UserDto> _userMapper;

    public ConfirmUserCodeUseCase(
        IAuthService authService, 
        IApplicationMapper<User, UserDto> userMapper, 
        IDbUnitOfWork dbUnitOfWork)
    {
        _authService = authService;
        _userMapper = userMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<ConfirmUserCodeResponseDto> Handle(ConfirmUserCodeRequestDto request, CancellationToken cancellationToken)
    {
        var user = await _authService.ConfirmUser(UserId.CreateFromString(request.UserId), request.ConfirmCode);
        var claims = await _dbUnitOfWork.RoleRepository.GetUserClaims(user.Id);
        
        return new ConfirmUserCodeResponseDto(
            _userMapper.MapToDto(user),
            claims.Select(c => c.Value).ToList());
    }
}