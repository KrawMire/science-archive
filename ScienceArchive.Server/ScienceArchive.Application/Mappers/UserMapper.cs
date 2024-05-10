using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Factories;

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
            Articles = articles,
            About = user.About
        };
    }

    public User MapToEntity(UserDto model)
    {
        var builder = new UserBuilder(model.Id);

        foreach (var article in model.Articles)
        {
            builder.AddArticle(article.ArticleId, article.Title);
        }

        return builder
            .AddName(model.Name)
            .AddEmail(model.Email)
            .AddLogin(model.Login)
            .AddAboutText(model.About)
            .Build();
    }
}