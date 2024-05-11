using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class DeleteNewsUseCase : IUseCase<DeleteNewsRequestDto, DeleteNewsResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    
    public DeleteNewsUseCase(IDbUnitOfWork dbUnitOfWork)
    {
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<DeleteNewsResponseDto> Execute(DeleteNewsRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var deletedNewsId = await _dbUnitOfWork.NewsRepository.Delete(newsId);

        return new DeleteNewsResponseDto(deletedNewsId.ToString());
    }
}