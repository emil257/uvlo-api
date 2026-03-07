using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
  }

}
