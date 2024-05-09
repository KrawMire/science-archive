using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class DeleteNewsUseCase : IUseCase<DeleteNewsRequestDto, DeleteNewsResponseDto>
{
    private readonly IDbContext _dbContext;
    
    public DeleteNewsUseCase(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DeleteNewsResponseDto> Execute(DeleteNewsRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var deletedNewsId = await _dbContext.NewsRepository.Delete(newsId);

        return new DeleteNewsResponseDto(deletedNewsId.ToString());
    }
}