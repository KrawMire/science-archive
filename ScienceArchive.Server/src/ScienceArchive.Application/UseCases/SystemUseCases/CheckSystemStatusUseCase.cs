using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.UseCases.SystemUseCases;

internal class CheckSystemStatusUseCase : IUseCase<CheckSystemStatusRequestDto, CheckSystemStatusResponseDto>
{
    public Task<CheckSystemStatusResponseDto> Handle(CheckSystemStatusRequestDto request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CheckSystemStatusResponseDto(true));
    }
}