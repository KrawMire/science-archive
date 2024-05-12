using ScienceArchive.Application.Abstractions.Templating;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Domain.Aggregates.Notification.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class ResendConfirmationCodeUseCase : IUseCase<ResendConfirmationCodeRequestDto, ResendConfirmationCodeResponseDto>
{
    private readonly ITemplateService _templateService;
    private readonly INotificationGateway _notificationGateway;
    private readonly IAuthService _authService;

    public ResendConfirmationCodeUseCase(
        INotificationGateway notificationGateway, 
        IAuthService authService, 
        ITemplateService templateService)
    {
        _notificationGateway = notificationGateway;
        _authService = authService;
        _templateService = templateService;
    }

    public async Task<ResendConfirmationCodeResponseDto> Handle(ResendConfirmationCodeRequestDto request, CancellationToken cancellationToken)
    {
        var (user, code) = await _authService.RegenerateConfirmCode(UserId.CreateFromString(request.UserId));

        if (code is null)
        {
            return new ResendConfirmationCodeResponseDto(user.Id.ToString());
        }
        
        var populatedEmail = await _templateService.GetPopulatedOtpEmailTemplate(user.Name, code);
        
        var notification = new Notification(NotificationId.CreateNew())
        {
            Message = populatedEmail,
            Receiver = user.Email
        };
        await _notificationGateway.SendNotification(notification);
        
        return new ResendConfirmationCodeResponseDto(user.Id.ToString());
    }
}