using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetUserByIdUseCase : IUseCase<GetUserByIdRequestDto, GetUserByIdResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public GetUserByIdUseCase(IApplicationMapper<User, UserDto> userMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _userMapper = userMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetUserByIdResponseDto> Handle(GetUserByIdRequestDto request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFromString(request.Id);
        var user = await _dbUnitOfWork.UserRepository.GetById(userId);

        var userDto = user is not null
            ? _userMapper.MapToDto(user)
            : null;

        return new GetUserByIdResponseDto(userDto);
    }
}