using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class DeleteUserUseCase : IUseCase<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    
    public DeleteUserUseCase(IDbUnitOfWork dbUnitOfWork)
    {
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<DeleteUserResponseDto> Handle(DeleteUserRequestDto request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFromString(request.Id);
        var initiatorUserId = UserId.CreateFromString(request.InitiatorUserId);

        if (!userId.Equals(initiatorUserId))
        {
            throw new InvalidInitiatorUserException();
        }
        
        var deletedUserId = await _dbUnitOfWork.UserRepository.Delete(userId);

        return new DeleteUserResponseDto(deletedUserId.ToString());
    }
}