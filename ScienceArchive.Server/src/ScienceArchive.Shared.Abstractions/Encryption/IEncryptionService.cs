namespace ScienceArchive.Shared.Abstractions.Encryption;

public interface IEncryptionService
{
    /// <summary>
    /// Generate new salt. Primary for passwords
    /// </summary>
    /// <param name="size"></param>
    /// <returns>Salt for password</returns>
    /// <exception cref="Exception"></exception>
    public string CreateSalt(int size = 64);

    /// <summary>
    /// Generate hash from string value
    /// </summary>
    /// <param name="password">String value to create hash from</param>
    /// <param name="salt">Password salt for hashing</param>
    /// <returns>Hashed value</returns>
    public string HashPassword(string password, string? salt = null);
}