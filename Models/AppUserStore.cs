
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using uvlo_api.Data;

namespace uvlo_api.Api.Data.Models
{
  public class AppUserStore : UserStore<AppUserModel, AppRoleModel, AppDbContext, int>
  {
    public AppUserStore(AppDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
  }
}
