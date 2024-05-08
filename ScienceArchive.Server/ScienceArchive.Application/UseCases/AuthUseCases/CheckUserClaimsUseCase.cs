using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Services;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class CheckUserClaimsUseCase : IUseCase<CheckUserClaimsRequestDto, CheckUserClaimsResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IRoleRepository _roleRepository;
    
    public CheckUserClaimsUseCase(IAuthService authService, IRoleRepository roleRepository)
    {
        _authService = authService;
        _roleRepository = roleRepository;
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