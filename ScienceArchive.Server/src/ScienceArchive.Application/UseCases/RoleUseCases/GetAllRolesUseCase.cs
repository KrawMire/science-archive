using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Role;

namespace ScienceArchive.Application.UseCases.RoleUseCases;

internal class GetAllRolesUseCase : IUseCase<GetAllRolesRequestDto, GetAllRolesResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<Role, RoleDto> _roleMapper;
    
    public GetAllRolesUseCase(IApplicationMapper<Role, RoleDto> roleMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _roleMapper = roleMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetAllRolesResponseDto> Handle(GetAllRolesRequestDto request, CancellationToken cancellationToken)
    {
        var roles = await _dbUnitOfWork.RoleRepository.GetAll();
        var rolesDtos = roles.Select(role => _roleMapper.MapToDto(role)).ToList();

        return new GetAllRolesResponseDto(rolesDtos);
    }
}