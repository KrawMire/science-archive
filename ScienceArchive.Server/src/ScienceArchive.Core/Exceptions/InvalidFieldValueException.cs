namespace ScienceArchive.Core.Exceptions;

public class InvalidFieldValueException : Exception
{
    public string InvalidFieldName { get; }
    
    public InvalidFieldValueException(string invalidFieldName)
    {
        InvalidFieldName = invalidFieldName;
    }
    
    public InvalidFieldValueException(string invalidFieldName, Exception? innerException) 
        : base(message: null, innerException)
    {
        InvalidFieldName = invalidFieldName;
    }
}