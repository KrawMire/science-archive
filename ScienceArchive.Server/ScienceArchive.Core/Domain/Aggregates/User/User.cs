using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Shared.Utils;

namespace ScienceArchive.Core.Domain.Aggregates.User;

/// <summary>
/// User entity
/// </summary>
public class User : Entity<UserId>
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private string _login = string.Empty;

    public User(UserId? id = null) : base(id ?? UserId.CreateNew())
    {
    }

    /// <summary>
    /// Set of user roles identifiers
    /// </summary>
    public required List<UserRole> Roles { get; init; }
    
    /// <summary>
    /// Name of the user
    /// </summary>
    public required string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFieldValueException(nameof(Name));
            }

            _name = value.Trim();
        }
    }

    /// <summary>
    /// E-mail of the user
    /// </summary>
    public required string Email
    {
        get => _email;
        set
        {
            if (!StringValidator.IsEmail(value))
            {
                throw new InvalidFieldValueException(nameof(Email));
            }

            _email = value.Trim();
        }
    }

    /// <summary>
    /// Login of the user which is used to authorize
    /// </summary>
    public required string Login
    {
        get => _login;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFieldValueException(nameof(Login));
            }

            _login = value.Trim();
        }
    }
    
    /// <summary>
    /// List of articles which are related to user
    /// </summary>
    public List<UserArticle> Articles { get; set; }
    
    /// <summary>
    /// Short user self-descriptive text
    /// </summary>
    public string? About { get; set; }
    
    /// <summary>
    /// User password
    /// </summary>
    public required UserPassword Password { get; set; }
}