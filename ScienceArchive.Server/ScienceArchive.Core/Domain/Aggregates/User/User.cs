using System.Net.Mail;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.User;

/// <summary>
/// User entity
/// </summary>
public class User : AggregateRoot<UserId>
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private string _login = string.Empty;
    private bool _isConfirmed = false;

    internal User(UserId? id = null) : base(id ?? UserId.CreateNew())
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
            if (!MailAddress.TryCreate(value, out _))
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
    /// Value indicating whether the user is confirmed.
    /// </summary>
    public required bool IsConfirmed
    {
        get => _isConfirmed; 
        init => _isConfirmed = value;
    }
    
    /// <summary>
    /// List of articles which are related to user
    /// </summary>
    public required List<UserArticle> Articles { get; set; }
    
    /// <summary>
    /// Short user self-descriptive text
    /// </summary>
    public string? About { get; set; }
    
    /// <summary>
    /// User password
    /// </summary>
    public UserPassword? Password { get; set; }

    /// <summary>
    /// Confirms the user.
    /// </summary>
    public void Confirm()
    {
        _isConfirmed = true;
    }

    /// <summary>
    /// Disconfirms the user.
    /// </summary>
    public void Disconfirm()
    {
        _isConfirmed = false;
    }
}