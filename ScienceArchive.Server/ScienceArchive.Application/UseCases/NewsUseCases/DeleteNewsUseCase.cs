using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class DeleteNewsUseCase : IUseCase<DeleteNewsRequestDto, DeleteNewsResponseDto>
{
    private readonly INewsRepository _newsRepository;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public DeleteNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, INewsRepository newsRepository)
    {
        _newsMapper = newsMapper;
        _newsRepository = newsRepository;
    }
    
    public async Task<DeleteNewsResponseDto> Execute(DeleteNewsRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var deletedNewsId = await _newsRepository.Delete(newsId);

        return new DeleteNewsResponseDto(deletedNewsId.ToString());
    }
}