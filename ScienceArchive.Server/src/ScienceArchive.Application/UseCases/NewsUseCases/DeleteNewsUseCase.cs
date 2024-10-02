using ScienceArchive.Shared.Abstractions.Persistence;
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
    
    public async Task<DeleteNewsResponseDto> Handle(DeleteNewsRequestDto request, CancellationToken cancellationToken)
    {
        var newsId = NewsId.CreateFromString(request.Id);
        var deletedNewsId = await _dbUnitOfWork.NewsRepository.Delete(newsId);

        return new DeleteNewsResponseDto(deletedNewsId.ToString());
    }
}