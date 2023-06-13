using FinanceApp.Context;
using FinanceApp.Repository;
using FinanceApp.Repository.InterfacesImpl;
using FinanceApp.Services;
using FinanceApp.Services.InterfacesImpl;
using FinanceApp.Utils.Security;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var logInConnString = builder.Configuration.GetConnectionString("DBLogin");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<FinanceAppContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAziendeRepository, AziendeRepository>();
builder.Services.AddScoped<IAziendeService, AziendeService>();
builder.Services.AddScoped<IMovimentiRepository, MovimentiRepository>();
builder.Services.AddScoped<IMovimentiService, MovimentiService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("TokenSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors(x =>
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

}
else
{

}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.UseAuthentication();

app.UseRouting();

app.Run();