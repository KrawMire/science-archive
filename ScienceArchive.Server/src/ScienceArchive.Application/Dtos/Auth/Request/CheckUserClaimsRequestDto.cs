using MediatR;
using ScienceArchive.Application.Dtos.Auth.Response;

namespace ScienceArchive.Application.Dtos.Auth.Request;

/// <summary>
/// Request DTO to check user claims
/// </summary>
public record CheckUserClaimsRequestDto(string UserId, List<string>? RequiredClaims) : IRequest<CheckUserClaimsResponseDto>;