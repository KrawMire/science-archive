using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Core.Domain.Services;

/// <summary>
/// Service interface for confirmation operations.
/// </summary>
public interface IConfirmationService
{
    /// <summary>
    /// Generates a confirmation code for a given user ID.
    /// </summary>
    /// <param name="userId">The user ID for which to generate the confirmation code.</param>
    /// <returns>The generated confirmation code.</returns>
    Task<string> GenerateConfirmationCode(UserId userId);

    /// <summary>
    /// Confirms the user's code for a given user ID.
    /// </summary>
    /// <param name="userId">The user ID for which to confirm the code.</param>
    /// <param name="code">The code to confirm.</param>
    /// <returns>A boolean indicating whether the code was confirmed successfully.</returns>
    Task<bool> ConfirmUserCode(UserId userId, string code);
}