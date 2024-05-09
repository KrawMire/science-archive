using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class GetAllNewsUseCase : IUseCase<GetAllNewsRequestDto, GetAllNewsResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public GetAllNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, IDbContext dbContext)
    {
        _newsMapper = newsMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllNewsResponseDto> Execute(GetAllNewsRequestDto contract)
    {
        var news = await _dbContext.NewsRepository.GetAll();
        var newsDtos = news.Select(newsEntity => _newsMapper.MapToDto(newsEntity)).ToList();

        return new GetAllNewsResponseDto(newsDtos);
    }
}