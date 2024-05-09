using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class UpdateUserUseCase : IUseCase<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public UpdateUserUseCase(IApplicationMapper<User, UserDto> userMapper, IDbContext dbContext)
    {
        _userMapper = userMapper;
        _dbContext = dbContext;
    }
    
    public async Task<UpdateUserResponseDto> Execute(UpdateUserRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var user = _userMapper.MapToEntity(contract.User);
        var updatedUser = await _dbContext.UserRepository.Update(userId, user);

        return new UpdateUserResponseDto(_userMapper.MapToDto(updatedUser));
    }
}