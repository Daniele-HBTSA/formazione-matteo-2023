using FinanceApp.Context;
using FinanceApp.Repository.Interfaces;
using FinanceApp.Repository.InterfacesImpl;
using FinanceApp.Services.Interfaces;
using FinanceApp.Services.InterfacesImpl;
using FinanceApp.Utils.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var logInConnString = builder.Configuration.GetConnectionString("DBLogin");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<FinanceAppContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAziendeRepository, AziendeRepository>();
builder.Services.AddScoped<IAziendeService, AziendeService>();
builder.Services.AddScoped<IMovimentiRepository, MovimentiRepository>();
builder.Services.AddScoped<IMovimentiService, MovimentiService>();

//Helper impl.
IConfigurationSection jwtSettingsSection = builder.Configuration.GetSection("TokenSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

//Chiave segreta
byte[] secretKey = Encoding.ASCII.GetBytes(jwtSettingsSection.Get<JwtSettings>().Secret);

//Servizio di autenticazione jwt
builder.Services.AddAuthentication(element =>
{
    element.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    element.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(element =>
{
    element.RequireHttpsMetadata = false;
    element.SaveToken = true;
    element.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuerSigningKey = false,
        ValidateAudience = false
    };
});


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
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseStaticFiles();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.UseRouting();

app.Run();