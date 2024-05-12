using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Core.Domain.Aggregates.User.Factories;

public class UserBuilder
{
    private UserId _id;
    private string _name = string.Empty;
    private string _login = string.Empty;
    private string _email = string.Empty;
    private bool _isConfirmed = false;
    private List<UserRole> _roles = new();
    private List<UserArticle> _articles = new();

    private string? _aboutText;
    private string? _password;
    private string? _passwordSalt;
    
    public UserBuilder() : this(UserId.CreateNew()) {}
    
    public UserBuilder(string? userId)
    {
        _id = userId is not null 
            ? UserId.CreateFromString(userId) 
            : UserId.CreateNew();
    }

    public UserBuilder(Guid? userId)
    {
        _id = userId is not null 
            ? UserId.CreateFromGuid((Guid)userId) 
            : UserId.CreateNew();
    }

    public UserBuilder(UserId? userId)
    {
        _id = userId ?? UserId.CreateNew();
    }

    public UserBuilder AddName(string name)
    {
        _name = name;
        return this;
    }

    public UserBuilder AddEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public UserBuilder AddLogin(string login)
    {
        _login = login;
        return this;
    }

    public UserBuilder AddRole(string roleId)
    {
        return AddRole(RoleId.CreateFromString(roleId));
    }
    
    public UserBuilder AddRole(Guid roleId)
    {
        return AddRole(RoleId.CreateFromGuid(roleId));
    }
    
    public UserBuilder AddRole(RoleId roleId)
    {
        _roles.Add(new UserRole
        {
            RoleId = roleId
        });

        return this;
    }

    public UserBuilder AddArticle(string articleId, string title)
    {
        return AddArticle(ArticleId.CreateFromString(articleId), title);
    }
    
    public UserBuilder AddArticle(Guid articleId, string title)
    {
        return AddArticle(ArticleId.CreateFromGuid(articleId), title);
    }
    
    public UserBuilder AddArticle(ArticleId articleId, string title)
    {
        _articles.Add(new UserArticle
        {
            ArticleId = articleId,
            Title = title,
        });

        return this;
    }

    public UserBuilder AddAboutText(string? aboutText)
    {
        _aboutText = aboutText;
        return this;
    }

    public UserBuilder AddIsConfirmed(bool isConfirmed)
    {
        _isConfirmed = isConfirmed;
        return this;
    }
    
    public UserBuilder AddPassword(string? password)
    {
        _password = password;
        return this;
    }
    
    public UserBuilder AddPasswordSalt(string? salt)
    {
        _passwordSalt = salt;
        return this;
    }

    public User Build()
    {
        return new User(_id)
        {
            Roles = _roles,
            Name = _name,
            Email = _email,
            Login = _login,
            Articles = _articles,
            IsConfirmed = _isConfirmed,
            About = _aboutText,
            Password = new UserPassword
            {
                Value = _password,
                Salt = _passwordSalt,
            }
        };
    }
}