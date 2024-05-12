using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

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
        var deletedUserId = await _dbUnitOfWork.UserRepository.Delete(userId);

        return new DeleteUserResponseDto(deletedUserId.ToString());
    }
}