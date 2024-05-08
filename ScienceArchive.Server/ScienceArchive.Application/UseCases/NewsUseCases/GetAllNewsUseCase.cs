using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class GetAllNewsUseCase : IUseCase<GetAllNewsRequestDto, GetAllNewsResponseDto>
{
    private readonly INewsRepository _newsRepository;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public GetAllNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, INewsRepository newsRepository)
    {
        _newsMapper = newsMapper;
        _newsRepository = newsRepository;
    }
    
    public async Task<GetAllNewsResponseDto> Execute(GetAllNewsRequestDto contract)
    {
        var news = await _newsRepository.GetAll();
        var newsDtos = news.Select(newsEntity => _newsMapper.MapToDto(newsEntity)).ToList();

        return new GetAllNewsResponseDto(newsDtos);
    }
}