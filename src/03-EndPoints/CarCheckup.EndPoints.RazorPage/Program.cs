using CarCheckup.Domain.Core.Entities.Configs;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
#region Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection("SiteSettings").Get<SiteSettings>();
builder.Services.AddSingleton(siteSettings);
#endregion

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<CarCheckupDbContext>(options => options.UseSqlServer(siteSettings.ConnectionStrings.SqlConnection));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
