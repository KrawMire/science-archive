namespace ScienceArchive.Application.Abstractions.Templating;

/// <summary>
/// Represents a service for managing templates.
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Gets the OTP (One-time Password) email template populated with the provided username and confirmation code.
    /// </summary>
    /// <param name="userName">The user name to be inserted into the email template.</param>
    /// <param name="confirmCode">The confirmation code to be inserted into the email template.</param>
    /// <returns>The populated OTP email template as a string.</returns>
    Task<string> GetPopulatedOtpEmailTemplate(string userName, string confirmCode);
}