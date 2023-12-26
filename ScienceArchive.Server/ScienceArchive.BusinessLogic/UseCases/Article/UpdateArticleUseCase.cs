using ScienceArchive.BusinessLogic.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Core.Services.ArticleContracts;

namespace ScienceArchive.BusinessLogic.ArticleUseCases;

internal class UpdateArticleUseCase : IUseCase<Article, UpdateArticleContract>
{
    private readonly IArticleRepository _articleRepository;

    public UpdateArticleUseCase(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
    }

    public Task<Article> Execute(UpdateArticleContract contract)
    {
        contract.Article.Status = ArticleStatus.ToVerify;
        return _articleRepository.Update(contract.Id, contract.Article);
    }
}