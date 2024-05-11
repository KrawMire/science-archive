using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class GetAllNewsUseCase : IUseCase<GetAllNewsRequestDto, GetAllNewsResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public GetAllNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _newsMapper = newsMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<GetAllNewsResponseDto> Execute(GetAllNewsRequestDto contract)
    {
        var news = await _dbUnitOfWork.NewsRepository.GetAll();
        var newsDtos = news.Select(newsEntity => _newsMapper.MapToDto(newsEntity)).ToList();

        return new GetAllNewsResponseDto(newsDtos);
    }
}