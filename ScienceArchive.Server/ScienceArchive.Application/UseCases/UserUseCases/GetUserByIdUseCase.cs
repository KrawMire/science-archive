using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetUserByIdUseCase : IUseCase<GetUserByIdRequestDto, GetUserByIdResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetUserByIdUseCase(IApplicationMapper<User, UserDto> userMapper, IDbContext dbContext)
    {
        _userMapper = userMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetUserByIdResponseDto> Execute(GetUserByIdRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var user = await _dbContext.UserRepository.GetById(userId);

        var userDto = user is not null
            ? _userMapper.MapToDto(user)
            : null;

        return new GetUserByIdResponseDto(userDto);
    }
}