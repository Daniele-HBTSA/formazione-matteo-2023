using FinanceApp.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var logInConnString = builder.Configuration.GetConnectionString("DBLogin");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IAutenticationService, AuthenticationService>();
//builder.Services.AddScoped<IRegistrationService, RegistrationService>();
//builder.Services.AddScoped<IGetTableService, GetTableService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<FinanceAppContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));

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

app.UseRouting();

app.Run();