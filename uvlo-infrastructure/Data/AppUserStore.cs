using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using uvlo_core.Entities;

public class AppUserStore : UserStore<AppUserModel, AppRoleModel, AppDbContext, int>
{
  public AppUserStore(AppDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
  {
  }
}
