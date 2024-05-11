using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("users", Schema = "user")]
[Index("Email", Name = "idx__users__email")]
[Index("Login", Name = "idx__users__login")]
internal partial class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("login")]
    [StringLength(255)]
    public string Login { get; set; } = null!;

    [Column("about")]
    public string? About { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<News> News { get; set; } = new List<News>();

    [InverseProperty("User")]
    public virtual ICollection<UsersArticle> UsersArticles { get; set; } = new List<UsersArticle>();

    [InverseProperty("User")]
    public virtual UsersAuth? UsersAuth { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
