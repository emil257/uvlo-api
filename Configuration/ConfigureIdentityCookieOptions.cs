
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class ConfigureIdentityCookieOptions : IConfigureNamedOptions<CookieAuthenticationOptions>
{
    private readonly ITicketStore _ticketStore;

    public ConfigureIdentityCookieOptions(ITicketStore ticketStore)
    {
        _ticketStore = ticketStore;
    }

    public void Configure(CookieAuthenticationOptions options) { }

    public void Configure(string? name, CookieAuthenticationOptions options)
    {
        if (name == IdentityConstants.ApplicationScheme) // Identity cookie
        {
            options.SessionStore = _ticketStore; // your Redis-backed store
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
            options.SlidingExpiration = true;
            options.Events.OnSigningIn = context =>
            {
                var domain = context.Request.Host.Host;
                if (domain.EndsWith("uvlo.com"))
                {
                    context.CookieOptions.Domain = ".uvlo.com";
                }
                return Task.CompletedTask;
            };
        }
    }
}
