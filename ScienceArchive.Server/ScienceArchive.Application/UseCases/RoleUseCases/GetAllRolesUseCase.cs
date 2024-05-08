using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;

namespace ScienceArchive.Application.UseCases.RoleUseCases;

internal class GetAllRolesUseCase : IUseCase<GetAllRolesRequestDto, GetAllRolesResponseDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IApplicationMapper<Role, RoleDto> _roleMapper;
    
    public GetAllRolesUseCase(IApplicationMapper<Role, RoleDto> roleMapper, IRoleRepository roleRepository)
    {
        _roleMapper = roleMapper;
        _roleRepository = roleRepository;
    }
    
    public async Task<GetAllRolesResponseDto> Execute(GetAllRolesRequestDto contract)
    {
        var roles = await _roleRepository.GetAll();
        var rolesDtos = roles.Select(role => _roleMapper.MapToDto(role)).ToList();

        return new GetAllRolesResponseDto(rolesDtos);
    }
}