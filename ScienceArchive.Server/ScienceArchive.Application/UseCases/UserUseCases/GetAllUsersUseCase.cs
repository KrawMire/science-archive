using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetAllUsersUseCase : IUseCase<GetAllUsersRequestDto, GetAllUsersResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetAllUsersUseCase(IApplicationMapper<User, UserDto> userMapper, IDbContext dbContext)
    {
        _userMapper = userMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllUsersResponseDto> Execute(GetAllUsersRequestDto contract)
    {
        var users = await _dbContext.UserRepository.GetAll();
        var usersDtos = users.Select(user => _userMapper.MapToDto(user)).ToList();
        
        return new GetAllUsersResponseDto(usersDtos);
    }
}