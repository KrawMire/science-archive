using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

[Table("claims", Schema = "auth")]
[Index("Value", Name = "idx__claims__value")]
public partial class Claim
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("value")]
    [StringLength(100)]
    public string Value { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [ForeignKey("ClaimId")]
    [InverseProperty("Claims")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
