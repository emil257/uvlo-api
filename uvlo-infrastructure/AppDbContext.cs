using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uvlo_core.Entities;

public partial class AppDbContext : IdentityDbContext<AppUserModel, AppRoleModel, int>
{
  public DbSet<Post> Posts { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Post>(entity => {
      entity.ToTable("AppPosts");

      entity.HasKey(p => p.Id);

      entity.HasQueryFilter(p => p.DeletedAt == null);

      entity.HasMany(p => p.Comments)
          .WithOne()
          .HasForeignKey(p => p.ParentId);

      entity.Property(p => p.Type)
          .HasConversion<string>()
          .HasMaxLength(20);

      entity.Property(p => p.Content)
          .HasMaxLength(550);
    });
  }

}
