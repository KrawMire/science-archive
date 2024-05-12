using ScienceArchive.Core.Domain.Common.Identifiers;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.Notification.ValueObjects;

public class NotificationId : EntityId<Guid>
{
    private NotificationId(Guid value) : base(value)
    {
    }

    /// <summary>
    /// Create new entity ID
    /// </summary>
    /// <returns>New instance of entity ID</returns>
    public static NotificationId CreateNew()
    {
    	return CreateFromGuid(Guid.NewGuid());
    }
    
    /// <summary>
    /// Create new entity ID from
    /// string value representation
    /// </summary>
    /// <param name="value">String ID representation</param>
    /// <returns>New instance of entity ID</returns>
    /// <exception cref="InvalidEntityIdValueException">
    /// Thrown if string value of ID is invalid
    /// </exception>
    public static NotificationId CreateFromString(string value)
    {
    	if (!Guid.TryParse(value, out var idValue))
    	{
    		throw new InvalidEntityIdValueException(value, nameof(NotificationId));
    	}
    	
    	return CreateFromGuid(idValue);
    }

    /// <summary>
    /// Create new entity ID with
    /// predefined value
    /// </summary>
    /// <param name="value">ID value</param>
    /// <returns>New instance of entity ID</returns>
    /// <exception cref="InvalidEntityIdValueException">
    /// Thrown if ID value is invalid
    /// </exception>
    public static NotificationId CreateFromGuid(Guid value)
    {
    	return new NotificationId(value);
    }

    /// <summary>
    /// Get string representation
    /// of ID value
    /// </summary>
    /// <returns>String representation of ID value</returns>
    public override string ToString()
    {
    	return Value.ToString();
    }
    
    /// <inheritdoc />
    public override bool Equals(EntityId<Guid> compareId)
    {
    	return compareId is NotificationId compareNotificationId && Value.Equals(compareNotificationId.Value);
    }
}