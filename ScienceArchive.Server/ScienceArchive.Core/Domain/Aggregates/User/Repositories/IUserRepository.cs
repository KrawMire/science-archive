using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.User.Repositories;

/// <summary>
/// User repository functionality
/// </summary>
public interface IUserRepository : ICrudRepository<UserId, User>
{
    /// <summary>
    /// Retrieves a user with the given login or email.
    /// </summary>
    /// <param name="login">Login or email address of the user</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> GetUserByLoginOrEmail(string login);

    /// <summary>
    /// Retrieves a user with the given login.
    /// </summary>
    /// <param name="login">Login address of the user</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> GetUserByLogin(string login);

    /// <summary>
    /// Retrieves a user with the given email address.
    /// </summary>
    /// <param name="email">Email address of the user</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> GetUserByEmail(string email);
}