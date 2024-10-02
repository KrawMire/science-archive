using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetAllUsersUseCase : IUseCase<GetAllUsersRequestDto, GetAllUsersResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetAllUsersUseCase(IApplicationMapper<User, UserDto> userMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _userMapper = userMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetAllUsersResponseDto> Handle(GetAllUsersRequestDto request, CancellationToken cancellationToken)
    {
        var users = await _dbUnitOfWork.UserRepository.GetAll();
        var usersDtos = users.Select(user => _userMapper.MapToDto(user)).ToList();
        
        return new GetAllUsersResponseDto(usersDtos);
    }
}