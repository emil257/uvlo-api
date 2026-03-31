using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uvlo_api.Data.Models.Database;

namespace uvlo_api.Data;

public partial class AppDbContext : IdentityDbContext<AppUserModel, AppRoleModel, int>
{
  public DbSet<Post> Posts { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.Entity<Post>()
        .HasQueryFilter(p => p.DeletedAt == null)
        .HasMany(p => p.Comments)
        .WithOne()
        .HasForeignKey(p => p.ParentId);
    builder.Entity<Post>()
        .Property(p => p.Type)
        .HasConversion<string>()
        .HasMaxLength(20);

  }

}
