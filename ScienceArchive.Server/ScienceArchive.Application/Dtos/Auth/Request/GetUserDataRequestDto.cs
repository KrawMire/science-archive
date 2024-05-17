using MediatR;
using ScienceArchive.Application.Dtos.Auth.Response;

namespace ScienceArchive.Application.Dtos.Auth.Request;

public record GetUserDataRequestDto(string UserId) : IRequest<GetUserDataResponseDto>;