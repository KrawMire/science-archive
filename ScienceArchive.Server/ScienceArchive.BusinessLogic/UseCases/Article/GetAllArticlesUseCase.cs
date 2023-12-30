using ScienceArchive.BusinessLogic.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Core.Services.ArticleContracts;

namespace ScienceArchive.BusinessLogic.ArticleUseCases;

public class GetAllArticlesUseCase : IUseCase<List<Article>, GetAllArticlesContract>
{
	private readonly IArticleRepository _articleRepository;
	
	public GetAllArticlesUseCase(IArticleRepository articleRepository)
	{
		_articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
	}
	
	public Task<List<Article>> Execute(GetAllArticlesContract contract)
	{
		return _articleRepository.GetAll();
	}
}