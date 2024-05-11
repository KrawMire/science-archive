using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class DeleteArticleUseCase : IUseCase<DeleteArticleRequestDto, DeleteArticleResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    
    public DeleteArticleUseCase(IDbUnitOfWork dbUnitOfWork)
    {
        _dbUnitOfWork = dbUnitOfWork;
    }
    
    public async Task<DeleteArticleResponseDto> Execute(DeleteArticleRequestDto contract)
    {
        var articleId = ArticleId.CreateFromString(contract.Id);
        var deletedArticleId = await _dbUnitOfWork.ArticleRepository.Delete(articleId);
        
        return new DeleteArticleResponseDto(deletedArticleId.ToString());
    }
}