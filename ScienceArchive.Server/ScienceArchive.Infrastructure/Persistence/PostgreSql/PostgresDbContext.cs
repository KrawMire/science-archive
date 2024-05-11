using Microsoft.EntityFrameworkCore;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

public partial class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticlesDocument> ArticlesDocuments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersArticle> UsersArticles { get; set; }

    public virtual DbSet<UsersAuth> UsersAuths { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("articles_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Articles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__articles__category_id");
        });

        modelBuilder.Entity<ArticlesDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("articles_documents_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Article).WithMany(p => p.ArticlesDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__articles_documents__article_id__articles__id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("claims_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Roles).WithMany(p => p.Claims)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesClaim",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk__roles_claims__role_id__roles__id"),
                    l => l.HasOne<Claim>().WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk__roles_claims__claim_id__claims__id"),
                    j =>
                    {
                        j.HasKey("ClaimId", "RoleId").HasName("roles_claims_pkey");
                        j.ToTable("roles_claims", "auth");
                        j.IndexerProperty<Guid>("ClaimId").HasColumnName("claim_id");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("news_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.News)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__news__author_id__users__id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subcategories_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk__users_roles__role_id"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk__users_roles__user_id"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("users_roles_pkey");
                        j.ToTable("users_roles", "auth");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UsersArticle>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ArticleId }).HasName("users_articles_pkey");

            entity.HasOne(d => d.Article).WithMany(p => p.UsersArticles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__users_articles__article_id__articles__id");

            entity.HasOne(d => d.User).WithMany(p => p.UsersArticles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__users_articles__user_id__users__id");
        });

        modelBuilder.Entity<UsersAuth>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_auth_pkey");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithOne(p => p.UsersAuth)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk__users_auth__user_id__users__id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
