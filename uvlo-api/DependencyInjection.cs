using uvlo_core.Entities;
public static class PresentationServiceCollectionExtensions
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddExceptionHandler<AppExceptionHandler>();
    services.AddExceptionHandler<GlobalExceptionHandler>();
    services.AddProblemDetails();

    services.AddIdentityApiEndpoints<AppUserModel>((options) => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<AppRoleModel>()
            .AddUserStore<AppUserStore>()
            .AddEntityFrameworkStores<AppDbContext>();

    //Configure cookie options for Identity
    services.ConfigureOptions<ConfigureIdentityCookieOptions>();

    return services;
  }
}
