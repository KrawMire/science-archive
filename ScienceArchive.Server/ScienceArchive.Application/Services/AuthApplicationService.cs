using MediatR;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Represents the authentication application service.
/// </summary>
internal class AuthApplicationService : BaseApplicationService, IAuthApplicationService
{ 
    public AuthApplicationService(IMediator mediator, IDbUnitOfWork dbUnitOfWork, IEventBus eventBus) 
        : base(mediator, dbUnitOfWork, eventBus) { }

    /// <inheritdoc/>
    public Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        return ExecuteTransactionalUseCase<LoginRequestDto, LoginResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<RegisterResponseDto> Register(RegisterRequestDto dto)
    {
        return ExecuteTransactionalUseCase<RegisterRequestDto, RegisterResponseDto>(dto);
    }

    /// <inheritdoc/>
    public Task<ConfirmUserCodeResponseDto> ConfirmUserCode(ConfirmUserCodeRequestDto dto)
    {
        return ExecuteTransactionalUseCase<ConfirmUserCodeRequestDto, ConfirmUserCodeResponseDto>(dto);
    }

    public Task<ResendConfirmationCodeResponseDto> ResendConfirmCode(ResendConfirmationCodeRequestDto dto)
    {
        return ExecuteTransactionalUseCase<ResendConfirmationCodeRequestDto, ResendConfirmationCodeResponseDto>(dto);
    }

    public Task<GetUserDataResponseDto> GetUserData(GetUserDataRequestDto dto)
    {
        return ExecuteUseCase<GetUserDataRequestDto, GetUserDataResponseDto>(dto);
    }


    /// <inheritdoc/>
    public Task<CheckUserClaimsResponseDto> CheckUserClaims(CheckUserClaimsRequestDto dto)
    {
        return ExecuteUseCase<CheckUserClaimsRequestDto, CheckUserClaimsResponseDto>(dto);
    }
}