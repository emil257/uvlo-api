using uvlo_api.Data.Models.Database;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddPersistenceInfrastructure(builder.Configuration)
                                .AddCachingInfrastructure(builder.Configuration)
                                .AddIdentityInfrastructure()
                                .AddDataProtectionInfrastructure()
                                .AddRepos()
                                .AddServices()
                                .AddPresentation();

var app = builder.Build();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors(builder => builder
  .WithOrigins("http://localhost:4200")
  .AllowAnyMethod()
  .AllowAnyHeader()
  .AllowCredentials());
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<AppUserModel>();

app.Run();

