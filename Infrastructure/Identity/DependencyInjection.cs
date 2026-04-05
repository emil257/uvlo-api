using Microsoft.AspNetCore.Authentication.Cookies;
using uvlo_api.Models.Database;
using uvlo_api.Data.Models.Database;
using uvlo_api.Data;
using uvlo_api.Services;

public static partial class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
  {

    services.AddIdentityApiEndpoints<AppUserModel>((options) => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<AppRoleModel>()
            .AddUserStore<AppUserStore>()
            .AddEntityFrameworkStores<AppDbContext>();

    services.AddSingleton<ITicketStore, AppTicketStore>();

    //Configure cookie options for Identity
    services.ConfigureOptions<ConfigureIdentityCookieOptions>();

    services.AddScoped<CurrentUserService>();

    return services;
  }
}
