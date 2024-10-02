using MediatR;
using ScienceArchive.Application.Dtos.System.Response;

namespace ScienceArchive.Application.Dtos.System.Request;

/// <summary>
/// Represents request for checking system status
/// </summary>
public record CheckSystemStatusRequestDto : IRequest<CheckSystemStatusResponseDto>;