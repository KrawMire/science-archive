using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[PrimaryKey("UserId", "ArticleId")]
[Table("users_articles", Schema = "article")]
public partial class UsersArticle
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Key]
    [Column("article_id")]
    public Guid ArticleId { get; set; }

    [Column("role")]
    public short Role { get; set; }

    [ForeignKey("ArticleId")]
    [InverseProperty("UsersArticles")]
    public virtual Article Article { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UsersArticles")]
    public virtual User User { get; set; } = null!;
}
