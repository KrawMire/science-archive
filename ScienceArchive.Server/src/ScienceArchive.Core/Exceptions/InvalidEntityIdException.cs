namespace ScienceArchive.Core.Exceptions;

/// <summary>
/// Represents exception if some errors occurred while creating entity ID
/// </summary>
public class InvalidEntityIdValueException : Exception
{
	public string? InvalidValue { get; }
	public string? EntityIdName { get; }
	
	public InvalidEntityIdValueException(string? invalidValue, string? entityIdName)
	{
		InvalidValue = invalidValue;
		EntityIdName = entityIdName;
	}

	public InvalidEntityIdValueException(string? invalidValue, string? entityIdName, Exception? innerException) 
		: base(message: null, innerException)
	{
		InvalidValue = invalidValue;
		EntityIdName = entityIdName;
	}
}