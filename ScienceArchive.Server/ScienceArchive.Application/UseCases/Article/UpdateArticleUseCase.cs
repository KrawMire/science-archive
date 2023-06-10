﻿using System;
using ScienceArchive.Core.Domain.Entities;
using ScienceArchive.Core.Dtos.Article;
using ScienceArchive.Core.Dtos.Article.Request;
using ScienceArchive.Core.Dtos.Article.Response;
using ScienceArchive.Core.Interfaces.Mappers;
using ScienceArchive.Core.Interfaces.Repositories;
using ScienceArchive.Core.Interfaces.UseCases;

namespace ScienceArchive.Application.ArticleUseCases
{
    public class UpdateArticleUseCase : IUseCase<UpdateArticleResponseDto, UpdateArticleRequestDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper<Article, ArticleDto> _articleMapper;

        public UpdateArticleUseCase(IArticleRepository articleRepository, IMapper<Article, ArticleDto> articleMapper)
        {
            if (articleRepository is null)
            {
                throw new ArgumentNullException(nameof(articleRepository));
            }

            if (articleMapper is null)
            {
                throw new ArgumentNullException(nameof(articleMapper));
            }

            _articleRepository = articleRepository;
            _articleMapper = articleMapper;
        }

        public async Task<UpdateArticleResponseDto> Execute(UpdateArticleRequestDto dto)
        {
            var newArticle = _articleMapper.MapToEntity(dto.NewArticle);
            var updatedArticle = await _articleRepository.Update(dto.Id, newArticle);

            return new(_articleMapper.MapToModel(updatedArticle));
        }
    }
}
