using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.UseCases.SystemUseCases;

internal class CheckSystemStatusUseCase : IUseCase<CheckSystemStatusRequestDto, CheckSystemStatusResponseDto>
{
    public Task<CheckSystemStatusResponseDto> Execute(CheckSystemStatusRequestDto contract)
    {
        return Task.FromResult(new CheckSystemStatusResponseDto(true));
    }
}