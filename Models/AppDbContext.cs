using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using uvlo_api.Api.Data.Models;

namespace uvlo_api.Data;

public partial class AppDbContext : IdentityDbContext<AppUserModel, AppRoleModel, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
		}

}
