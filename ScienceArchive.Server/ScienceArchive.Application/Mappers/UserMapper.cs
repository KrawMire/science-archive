using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.Mappers;

internal class UserMapper : IApplicationMapper<User, UserDto>
{
    public UserDto MapToDto(User user)
    {
        var articles = user.Articles
            .Select(a => new UserArticleDto
            {
                ArticleId = a.ArticleId.ToString(),
                Title = a.Title,
            }).ToList();
        
        return new UserDto
        {
            Id = user.Id.ToString(),
            Name = user.Name,
            Email = user.Email,
            Login = user.Login,
            Articles = articles
        };
    }

    public User MapToEntity(UserDto model)
    {
        var userId = string.IsNullOrWhiteSpace(model.Id)
            ? null
            : UserId.CreateNew();

        var articles = model.Articles
            .Select(a => new UserArticle
            {
                ArticleId = ArticleId.CreateFromString(a.ArticleId),
                Title = a.Title
            }).ToList();
        
        return new User(userId)
        {
            Name = model.Name,
            Email = model.Email,
            Login = model.Login,
            Articles = articles,
            Roles = new List<UserRole>(),
            Password = new UserPassword()
        };
    }
}