using ScienceArchive.BusinessLogic.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Core.Services.ArticleContracts;

namespace ScienceArchive.BusinessLogic.ArticleUseCases;

public class ApproveArticleUseCase : IUseCase<Article, ApproveArticleContract>
{
	private IArticleRepository _articleRepository;

	public ApproveArticleUseCase(IArticleRepository articleRepository)
	{
		_articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
	}
	
	public async Task<Article> Execute(ApproveArticleContract contract)
	{
		var articleToApprove = await _articleRepository.GetById(contract.ArticleId);

		if (articleToApprove is null)
		{
			throw new Exception("Cannot find article with specified ID");
		}
		
		articleToApprove.Approve();

		return await _articleRepository.Update(articleToApprove.Id, articleToApprove);
	}
}