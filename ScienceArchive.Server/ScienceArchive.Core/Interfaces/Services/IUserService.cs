using System;
using ScienceArchive.Core.Entities;

namespace ScienceArchive.Core.Interfaces.Services
{
    /// <summary>
    /// Represents user service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="newUser">New user to create</param>
        /// <returns>Created user</returns>
        Task<User> Create(User newUser);

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="userId">User ID to update</param>
        /// <param name="newUser">New user data</param>
        /// <returns>Updated user</returns>
        Task<User> Update(Guid userId, User newUser);

        /// <summary>
        /// Delete existing user
        /// </summary>
        /// <param name="userId">User ID to delete</param>
        /// <returns>Deleted user ID</returns>
        Task<Guid> Delete(Guid userId);
    }
}

