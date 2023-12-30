using ScienceArchive.BusinessLogic.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Core.Services.ArticleContracts;

namespace ScienceArchive.BusinessLogic.ArticleUseCases;

public class DeclineArticleUseCase : IUseCase<Article, DeclineArticleContract>
{
	private readonly IArticleRepository _articleRepository;
	
	public DeclineArticleUseCase(IArticleRepository articleRepository)
	{
		_articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
	}
	
	public async Task<Article> Execute(DeclineArticleContract contract)
	{
		var articleToApprove = await _articleRepository.GetById(contract.ArticleId);

		if (articleToApprove is null)
		{
			throw new Exception("Cannot find article with specified ID");
		}
		
		articleToApprove.Decline();

		return await _articleRepository.Update(articleToApprove.Id, articleToApprove);
	}
}