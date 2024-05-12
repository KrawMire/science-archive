using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("users_auth", Schema = "auth")]
internal partial class UsersAuth
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("password_salt")]
    [StringLength(255)]
    public string PasswordSalt { get; set; } = null!;

    [Column("is_confirmed")]
    public bool IsConfirmed { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UsersAuth")]
    public virtual User User { get; set; } = null!;
}
