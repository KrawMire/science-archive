using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("articles", Schema = "article")]
[Index("Title", Name = "idx__articles__title")]
internal partial class Article
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("status")]
    public short Status { get; set; }

    [Column("creation_date", TypeName = "timestamp without time zone")]
    public DateTime CreationDate { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Article")]
    public virtual ICollection<ArticlesDocument> ArticlesDocuments { get; set; } = new List<ArticlesDocument>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Articles")]
    public virtual Subcategory Category { get; set; } = null!;

    [InverseProperty("Article")]
    public virtual ICollection<UsersArticle> UsersArticles { get; set; } = new List<UsersArticle>();
}
