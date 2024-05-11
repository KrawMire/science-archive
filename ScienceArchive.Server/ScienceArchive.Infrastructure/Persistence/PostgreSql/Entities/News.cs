using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("news", Schema = "news")]
[Index("Title", Name = "idx__news__title")]
public partial class News
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("author_id")]
    public Guid AuthorId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("body")]
    public string Body { get; set; } = null!;

    [Column("creation_date", TypeName = "timestamp without time zone")]
    public DateTime CreationDate { get; set; }

    [Column("last_updated_date", TypeName = "timestamp without time zone")]
    public DateTime? LastUpdatedDate { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("News")]
    public virtual User Author { get; set; } = null!;
}
