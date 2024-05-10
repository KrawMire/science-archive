using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Factories;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.PostgreSql.PersistenceMappers;

internal class UserMapper : IInfrastructureMapper<User, UserModel>
{
    public UserModel MapToModel(User user)
    {
        var roles = user.Roles
            .Select(r => new UserRoleModel
            {
                RoleId = r.RoleId.Value
            }).ToList();

        var articles = user.Articles
            .Select(a => new UserArticleModel()
            {
                ArticleId = a.ArticleId.Value,
                Title = a.Title
            }).ToList();
        
        return new UserModel
        {
            Id = user.Id.Value,
            Roles = roles,
            Email = user.Email,
            Login = user.Login,
            Name = user.Name,
            Articles = articles,
            About = user.About,
            Password = user.Password.Value,
            PasswordSalt = user.Password.Salt,
        };
    }

    public User MapToEntity(UserModel model)
    {
        var builder = new UserBuilder(model.Id);

        if (model.Roles is not null)
        {
            foreach (var role in model.Roles)
            {
                builder.AddRole(role.RoleId);
            }   
        }
        
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