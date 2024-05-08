using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class UpdateNewsUseCase : IUseCase<UpdateNewsRequestDto, UpdateNewsResponseDto>
{
    private readonly INewsRepository _newsRepository;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public UpdateNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, INewsRepository newsRepository)
    {
        _newsMapper = newsMapper;
        _newsRepository = newsRepository;
    }
    
    public async Task<UpdateNewsResponseDto> Execute(UpdateNewsRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var news = _newsMapper.MapToEntity(contract.News);
        var updatedNews = await _newsRepository.Update(newsId, news);

        return new UpdateNewsResponseDto(_newsMapper.MapToDto(updatedNews));
    }
}