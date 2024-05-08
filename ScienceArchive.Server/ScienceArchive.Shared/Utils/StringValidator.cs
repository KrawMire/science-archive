using System.Net.Mail;

namespace ScienceArchive.Shared.Utils;

/// <summary>
/// Strings validation helper
/// </summary>
public static class StringValidator
{
    /// <summary>
    /// Is string email address
    /// </summary>
    /// <param name="value">Value to be checked</param>
    /// <returns>True if value is a valid email, otherwise, false</returns>
    public static bool IsEmail(string value)
    {
        return MailAddress.TryCreate(value, out _);
    }
}