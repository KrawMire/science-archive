using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Exceptions;
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

    public async Task<CheckUserClaimsResponseDto> Handle(CheckUserClaimsRequestDto request, CancellationToken cancellationToken)
    {
        if (request.RequiredClaims is null)
        {
            return new CheckUserClaimsResponseDto(true);
        }
        
        var userId = UserId.CreateFromString(request.UserId);
        var requiredClaims = await _dbUnitOfWork.RoleRepository.GetClaimsByValues(request.RequiredClaims);

        if (requiredClaims.Count != request.RequiredClaims.Count)
        {
            throw new CannotFindAllClaimsException(
                request.RequiredClaims, 
                requiredClaims.Select(rc => rc.Value).ToList());
        }
        
        var hasAllClaims = await _authService.UserHasClaims(userId, requiredClaims);
        return new CheckUserClaimsResponseDto(hasAllClaims);
    }
}