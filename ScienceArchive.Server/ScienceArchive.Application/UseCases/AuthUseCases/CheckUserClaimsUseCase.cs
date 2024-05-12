using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class CheckUserClaimsUseCase : IUseCase<CheckUserClaimsRequestDto, CheckUserClaimsResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IDbUnitOfWork _dbUnitOfWork;
    
    public CheckUserClaimsUseCase(IAuthService authService, IDbUnitOfWork dbUnitOfWork)
    {
        _authService = authService;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public Task<CheckUserClaimsResponseDto> Handle(CheckUserClaimsRequestDto request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFromString(request.UserId);
        throw new NotImplementedException();
    }
}