using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class GetNewsByIdUseCase : IUseCase<GetNewsByIdRequestDto, GetNewsByIdResponseDto>
{
    private readonly INewsRepository _newsRepository;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public GetNewsByIdUseCase(IApplicationMapper<News, NewsDto> newsMapper, INewsRepository newsRepository)
    {
        _newsMapper = newsMapper;
        _newsRepository = newsRepository;
    }
    
    public async Task<GetNewsByIdResponseDto> Execute(GetNewsByIdRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var news = await _newsRepository.GetById(newsId);

        var newsDto = news is not null
            ? _newsMapper.MapToDto(news)
            : null;

        return new GetNewsByIdResponseDto(newsDto);
    }
}