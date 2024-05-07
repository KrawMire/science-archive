using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.User.Repositories;

/// <summary>
/// User repository functionality
/// </summary>
public interface IUserRepository : ICrudRepository<UserId, User> { }