using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class DeleteUserUseCase : IUseCase<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IDbContext _dbContext;
    
    public DeleteUserUseCase(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DeleteUserResponseDto> Execute(DeleteUserRequestDto contract)
    {
        var userId = UserId.CreateFromString(contract.Id);
        var deletedUserId = await _dbContext.UserRepository.Delete(userId);

        return new DeleteUserResponseDto(deletedUserId.ToString());
    }
}