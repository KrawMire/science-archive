using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Role.Repositories;

/// <summary>
/// Role repository functionality
/// </summary>
public interface IRoleRepository : ICrudRepository<RoleId, Role>
{
	/// <summary>
	/// Get user claims
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <returns>List of user claims</returns>
	Task<List<RoleClaim>> GetUserClaims(UserId userId);
}