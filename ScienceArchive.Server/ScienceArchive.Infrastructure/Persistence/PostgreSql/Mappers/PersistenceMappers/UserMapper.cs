using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
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
        var userId = UserId.CreateFromGuid(model.Id);
        
        var roles = model.Roles is not null
            ? model.Roles.Select(r => new UserRole
            {
                RoleId = RoleId.CreateFromGuid(r.RoleId)
            }).ToList()
            : new List<UserRole>();

        var articles = model.Articles
            .Select(a => new UserArticle
            {
                ArticleId = ArticleId.CreateFromGuid(a.ArticleId),
                Title = a.Title
            }).ToList();
        
        return new User(userId)
        {
            Roles = roles, 
            Email = model.Email,
            Login = model.Login,
            Name = model.Name,
            Articles = articles,
            About = model.About,
            Password = new UserPassword
            {
                Value = model.Password ?? "",
                Salt = model.PasswordSalt ?? "", 
            }
        };
    }
}