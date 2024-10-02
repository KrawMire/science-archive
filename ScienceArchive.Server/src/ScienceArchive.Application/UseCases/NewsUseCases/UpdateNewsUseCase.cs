using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class UpdateNewsUseCase : IUseCase<UpdateNewsRequestDto, UpdateNewsResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public UpdateNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _newsMapper = newsMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<UpdateNewsResponseDto> Handle(UpdateNewsRequestDto request, CancellationToken cancellationToken)
    {
        var newsId = NewsId.CreateFromString(request.Id);
        var news = _newsMapper.MapToEntity(request.News);
        var updatedNews = await _dbUnitOfWork.NewsRepository.Update(newsId, news);

        return new UpdateNewsResponseDto(_newsMapper.MapToDto(updatedNews));
    }
}