using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using uvlo_api.Data;
using uvlo_api.Api.Data.Models;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddOpenApi();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("REDIS_CONNECTIONSTRING");
});

builder.Services.AddSingleton<ITicketStore, AppTicketStore>();

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

//Only for development, in production use a secure key management solution like Azure Key Vault or AWS KMS
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.aspnet/DataProtection-Keys"));

//Configure cookie options for Identity
builder.Services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureIdentityCookieOptions>();

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

