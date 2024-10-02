namespace ScienceArchive.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when all claims cannot be found.
/// </summary>
public class CannotFindAllClaimsException : Exception
{
    public CannotFindAllClaimsException(List<string> requiredClaims, List<string> foundClaims)
        : this($"Required claims: {string.Join(',', requiredClaims)}, but found only: {string.Join(',', foundClaims)}")
    {
        
    }

    public CannotFindAllClaimsException(string message) : base(message)
    {
        
    }
}