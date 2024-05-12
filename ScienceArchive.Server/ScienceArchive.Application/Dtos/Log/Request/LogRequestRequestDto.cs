using MediatR;
using ScienceArchive.Application.Abstractions.Logging.Models;
using ScienceArchive.Application.Dtos.Log.Response;

namespace ScienceArchive.Application.Dtos.Log.Request;

public record LogRequestRequestDto(RequestLog RequestLog) : IRequest<LogRequestResponseDto>;