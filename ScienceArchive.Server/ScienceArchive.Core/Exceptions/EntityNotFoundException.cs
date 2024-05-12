namespace ScienceArchive.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an entity cannot be found.
/// </summary>
public class EntityNotFoundException : Exception
{
    public string EntityName { get; }
	
    public EntityNotFoundException(string entityName)
    {
        EntityName = entityName;
    }
}