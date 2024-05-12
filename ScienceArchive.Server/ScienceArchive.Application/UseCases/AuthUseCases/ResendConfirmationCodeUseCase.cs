using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class ResendConfirmationCodeUseCase : IUseCase<ResendConfirmationCodeRequestDto, ResendConfirmationCodeResponseDto>
{
    private readonly INotificationGateway _notificationGateway;
    private readonly IAuthService _authService;

    public ResendConfirmationCodeUseCase(INotificationGateway notificationGateway, IAuthService authService)
    {
        _notificationGateway = notificationGateway;
        _authService = authService;
    }

    public async Task<ResendConfirmationCodeResponseDto> Execute(ResendConfirmationCodeRequestDto contract)
    {
        var (user, code) = await _authService.RegenerateConfirmCode(UserId.CreateFromString(contract.UserId));
        // await _notificationGateway.SendNotification(contract.UserId, code);
        return new ResendConfirmationCodeResponseDto(user.Id.ToString());
    }
}