using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("articles_documents", Schema = "article")]
public partial class ArticlesDocument
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("article_id")]
    public Guid ArticleId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("filepath")]
    [StringLength(255)]
    public string Filepath { get; set; } = null!;

    [ForeignKey("ArticleId")]
    [InverseProperty("ArticlesDocuments")]
    public virtual Article Article { get; set; } = null!;
}
