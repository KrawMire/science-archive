using System.Security.Cryptography;
using System.Text;
using ScienceArchive.Shared.Abstractions.Encryption;

namespace ScienceArchive.Infrastructure.Services;

/// <summary>
/// Provides encryption services for generating salt and hashing passwords.
/// </summary>
internal class EncryptionService : IEncryptionService
{
    /// <summary>
    /// Generate new salt. Primary for passwords
    /// </summary>
    /// <param name="size">The size of the salt to generate. Default value is 64.</param>
    /// <returns>Salt for password</returns>
    /// <exception cref="Exception">Thrown if unable to generate or convert the salt.</exception>
    public string CreateSalt(int size = 64)
    {
        var byteSalt = RandomNumberGenerator.GetBytes(size);
        _ = byteSalt ?? throw new Exception("Cannot generate password salt");
        
        var salt = Convert.ToBase64String(byteSalt);
        _ = salt ?? throw new Exception("Cannot convert salt to string");
        
        return salt;
    }

    /// <summary>
    /// Generates a hash from a given password and salt.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <param name="salt">The optional salt value for hashing. If not provided, a new salt will be generated.</param>
    /// <returns>The hashed value of the password.</returns>
    public string HashPassword(string password, string? salt = null)
    {
        salt ??= CreateSalt();

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(salt),
            10000,
            HashAlgorithmName.SHA256,
            64
        );
        
        return Convert.ToBase64String(hash);
    }
}