using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class CreateNewsUseCase : IUseCase<CreateNewsRequestDto, CreateNewsResponseDto>
{
    private readonly INewsRepository _newsRepository;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public CreateNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, INewsRepository newsRepository)
    {
        _newsMapper = newsMapper;
        _newsRepository = newsRepository;
    }
    
    public async Task<CreateNewsResponseDto> Execute(CreateNewsRequestDto contract)
    {
        var newsToCreate = _newsMapper.MapToEntity(contract.News);
        var createdNews = await _newsRepository.Create(newsToCreate);

        return new CreateNewsResponseDto(_newsMapper.MapToDto(createdNews));
    }
}