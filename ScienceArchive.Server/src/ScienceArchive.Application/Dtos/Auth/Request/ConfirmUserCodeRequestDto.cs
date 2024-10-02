using MediatR;
using ScienceArchive.Application.Dtos.Auth.Response;

namespace ScienceArchive.Application.Dtos.Auth.Request;

public record ConfirmUserCodeRequestDto(string UserId, string ConfirmCode) : IRequest<ConfirmUserCodeResponseDto>;