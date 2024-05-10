using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class CheckUserClaimsUseCase : IUseCase<CheckUserClaimsRequestDto, CheckUserClaimsResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IDbContext _dbContext;
    
    public CheckUserClaimsUseCase(IAuthService authService, IDbContext dbContext)
    {
        _authService = authService;
        _dbContext = dbContext;
    }
    
    public async Task<CheckUserClaimsResponseDto> Execute(CheckUserClaimsRequestDto contract)
    {
        throw new NotImplementedException();

        // var userClaims = await _roleService.GetUserClaims(getClaimsContract);
        //
        // if (dto.RequiredClaims.Any(requiredClaim => !userClaims.Exists(uc => uc.Value == requiredClaim)))
        // {
        //     return new CheckUserClaimsResponseDto(false);
        // }
        //
        // return new CheckUserClaimsResponseDto(true);
    }
}