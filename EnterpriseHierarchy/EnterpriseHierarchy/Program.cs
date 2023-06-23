using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Services.Implementations;
using EnterpriseHierarchy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

var logInConnString = builder.Configuration.GetConnectionString("DbLogin");
builder.Services.AddDbContext<EnterpriseHierarchyContext>(x => x.UseSqlServer(logInConnString, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }));

//Aggiungere gli scope delle interfacce
builder.Services.AddScoped<ITreeService, TreeService>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
