using System.Text.Json.Serialization;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

public class SubcategoryModel
{
	[JsonPropertyName("id")]
	public required Guid Id { get; set; }
	
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}