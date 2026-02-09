using Microsoft.EntityFrameworkCore;
using uvlo_api.Data;
using uvlo_api.Api.Data.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddOpenApi();


builder.Services.AddIdentityApiEndpoints<AppUserModel>((options) => options.SignIn.RequireConfirmedAccount = true)
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

