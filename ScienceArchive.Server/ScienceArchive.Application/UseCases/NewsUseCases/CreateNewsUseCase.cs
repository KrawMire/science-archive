using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;

namespace ScienceArchive.Application.UseCases.NewsUseCases;

internal class CreateNewsUseCase : IUseCase<CreateNewsRequestDto, CreateNewsResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<News, NewsDto> _newsMapper;
    
    public CreateNewsUseCase(IApplicationMapper<News, NewsDto> newsMapper, IDbUnitOfWork dbUnitOfWork)
    {
        _newsMapper = newsMapper;
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<CreateNewsResponseDto> Execute(CreateNewsRequestDto contract)
    {
        var newsToCreate = _newsMapper.MapToEntity(contract.News);
        var createdNews = await _dbUnitOfWork.NewsRepository.Create(newsToCreate);

        return new CreateNewsResponseDto(_newsMapper.MapToDto(createdNews));
    }
}