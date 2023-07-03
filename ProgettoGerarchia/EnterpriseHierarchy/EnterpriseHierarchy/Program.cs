using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Repository.Implementations;
using EnterpriseHierarchy.Repository.Interfaces;
using EnterpriseHierarchy.Services.Implementations;
using EnterpriseHierarchy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var logInConnString = builder.Configuration.GetConnectionString("DbLogin");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<EnterpriseHierarchyContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));

//Aggiungere gli scope delle interfacce
builder.Services.AddScoped<IEnterpricesRepository, EnterpricesRepository>();
builder.Services.AddScoped<IEnterpriseTreeService, EnterpriseTreeService>();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

} else
{
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.Run();
