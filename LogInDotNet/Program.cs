using LogInDotNet.Context;
using LogInDotNet.Repository;
using LogInDotNet.Repository.InterfacesImpl;
using LogInDotNet.Service;
using LogInDotNet.Service.InterfacesImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
var logInConnString = builder.Configuration.GetConnectionString("DBLogin");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAutenticationService, AuthenticationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IGetTableService, GetTableService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<LogInContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapGet("/selectUsers", async (UserController db) => await db.SelectUsers());

//app.MapPost("/newUser", async (UserController db) =>
//        await db.NewUser());


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

//app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

app.MapControllers();

app.UseRouting();

app.Run();








