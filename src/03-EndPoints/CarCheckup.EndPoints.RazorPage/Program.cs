using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Entities.Configs;
using CarCheckup.Domain.Services.AppService;
using CarCheckup.Domain.Services.Service;
using CarCheckup.Infra.EfCore.Common;
using CarCheckup.Infra.EfCore.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
#region Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection("SiteSettings").Get<SiteSettings>();
if (siteSettings != null)
    builder.Services.AddSingleton(siteSettings);
#endregion

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<CarCheckupDbContext>(options => options.UseSqlServer(siteSettings!.ConnectionStrings.SqlConnection));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddIdentity<Operator, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = false;


}).AddEntityFrameworkStores<CarCheckupDbContext>();


builder.Services.AddScoped<ICheckupRequestAppService, CheckupRequestAppService>();
builder.Services.AddScoped<ICheckupRequestService, CheckupRequestService>();
builder.Services.AddScoped<ICheckupRequestRepository, CheckupRequestRepository>();
builder.Services.AddScoped<IRejectedCheckupRequestService, RejectedCheckupRequestService>();
builder.Services.AddScoped<IRejectedCheckupRequestRepository, RejectedCheckupRequestRepository>();
builder.Services.AddScoped<ICarAppService, CarAppService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarModelAppService, CarModelAppService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<IOperatorAppService, OperatorAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
