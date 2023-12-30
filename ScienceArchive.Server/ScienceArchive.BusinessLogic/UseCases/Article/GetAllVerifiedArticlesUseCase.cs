﻿using ScienceArchive.BusinessLogic.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Core.Services.ArticleContracts;

namespace ScienceArchive.BusinessLogic.ArticleUseCases;

internal class GetAllVerifiedArticlesUseCase : IUseCase<List<Article>, GetAllVerifiedArticlesContract>
{
    private readonly IArticleRepository _articleRepository;

    public GetAllVerifiedArticlesUseCase(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
    }

    public async Task<List<Article>> Execute(GetAllVerifiedArticlesContract contract)
    {
        return await _articleRepository.GetAllVerified();
    }
}