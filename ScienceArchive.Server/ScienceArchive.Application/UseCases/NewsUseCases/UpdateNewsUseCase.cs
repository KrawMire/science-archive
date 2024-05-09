using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class UpdateNewsUseCase : IUseCase<UpdateNewsRequestDto, UpdateNewsResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public UpdateNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, IDbContext dbContext)
    {
        _newsMapper = newsMapper;
        _dbContext = dbContext;
    }
    
    public async Task<UpdateNewsResponseDto> Execute(UpdateNewsRequestDto contract)
    {
        var newsId = NewsId.CreateFromString(contract.Id);
        var news = _newsMapper.MapToEntity(contract.News);
        var updatedNews = await _dbContext.NewsRepository.Update(newsId, news);

        return new UpdateNewsResponseDto(_newsMapper.MapToDto(updatedNews));
    }
}