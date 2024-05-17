using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.ArticleUseCases;

internal class DeleteArticleUseCase : IUseCase<DeleteArticleRequestDto, DeleteArticleResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    
    public DeleteArticleUseCase(IDbUnitOfWork dbUnitOfWork)
    {
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task<DeleteArticleResponseDto> Handle(DeleteArticleRequestDto request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.CreateFromString(request.Id);
        var userId = UserId.CreateFromString(request.UserId);
        var article = await _dbUnitOfWork.ArticleRepository.GetById(articleId);

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        if (!article.Authors.Any(a => a.Role == ArticleAuthorRole.Owner && a.UserId.Equals(userId)))
        {
            throw new IncorrectArticleCreatorException();
        }
        
        var deletedArticleId = await _dbUnitOfWork.ArticleRepository.Delete(articleId);
        
        return new DeleteArticleResponseDto(deletedArticleId.ToString());
    }
}