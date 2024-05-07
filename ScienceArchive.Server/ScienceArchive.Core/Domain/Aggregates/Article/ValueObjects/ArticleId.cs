using ScienceArchive.Core.Domain.Common.Identifiers;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

/// <summary>
/// Identifier of an article
/// </summary>
public sealed class ArticleId : EntityId<Guid>
{
	private ArticleId(Guid value) : base(value)
	{
	}

	/// <summary>
	/// Create new entity ID
	/// </summary>
	/// <returns>New instance of entity ID</returns>
	public static ArticleId CreateNew()
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
	public static ArticleId CreateFromString(string value)
	{
		if (!Guid.TryParse(value, out var idValue))
		{
			throw new InvalidEntityIdValueException(value, nameof(ArticleId));
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
	public static ArticleId CreateFromGuid(Guid value)
	{
		return new ArticleId(value);
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
		return compareId is ArticleId compareArticleId && Value.Equals(compareArticleId.Value);
	}
}
