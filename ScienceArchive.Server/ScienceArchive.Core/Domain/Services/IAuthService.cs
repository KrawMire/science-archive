using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Core.Domain.Services;

/// <summary>
/// Represents the authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register a new user with the given user details
    /// </summary>
    /// <param name="user">The user to register</param>
    /// <param name="password">Password of user</param>
    /// <returns>The registered user</returns>
    Task<User> RegisterUser(User user, string password);

    /// <summary>
    /// Authorize user with the given login and password
    /// </summary>
    /// <param name="login">User login</param>
    /// <param name="password">User password</param>
    /// <returns>The authorized user if the login and password are valid, null otherwise</returns>
    Task<User?> AuthorizeUser(string login, string password);

    /// <summary>
    /// Determines whether a user has a specific set of claims.
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="claims">The claims to check</param>
    /// <returns>True if the user has all the specified claims, false otherwise</returns>
    Task<bool> UserHasClaims(UserId userId, IEnumerable<RoleClaim> claims);
}