using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class UpdateUserUseCase : IUseCase<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<User, UserDto> _userMapper;
    
    public UpdateUserUseCase(IApplicationMapper<User, UserDto> userMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _userMapper = userMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<UpdateUserResponseDto> Handle(UpdateUserRequestDto request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFromString(request.Id);
        var initiatorUserId = UserId.CreateFromString(request.InitiatorUserId);
        
        if (!userId.Equals(initiatorUserId))
        {
            throw new InvalidInitiatorUserException();
        }
        
        var user = _userMapper.MapToEntity(request.User);
        var updatedUser = await _dbUnitOfWork.UserRepository.Update(userId, user);

        return new UpdateUserResponseDto(_userMapper.MapToDto(updatedUser));
    }
}