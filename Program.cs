using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;
using uvlo_api.Api.Data.Models;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddOpenApi();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("REDIS_CONNECTIONSTRING");
});

builder.Services.AddScoped<ITicketStore, AppTicketStore>();

builder.Services.AddIdentityApiEndpoints<AppUserModel>((options) => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<AppRoleModel>()
        .AddUserStore<AppUserStore>()
        .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PSQL_CONNECTIONSTRING"))
        .EnableSensitiveDataLogging());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SessionStore = builder.Services.BuildServiceProvider().GetRequiredService<ITicketStore>();
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
		options.SlidingExpiration = true;
    options.Events.OnSigningIn = context =>
    {
        var request = context.Request;
        var domain = request.Host.Host;
        // if(domain.EndsWith("uvlo.com")){
        // 	context.CookieOptions.Domain = ".uvlo.com";
        // }
        return Task.CompletedTask;
    };
});


builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<AppUserModel>();

app.Run();

