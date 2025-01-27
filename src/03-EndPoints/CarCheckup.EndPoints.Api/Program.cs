using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities.Configs;
using CarCheckup.Domain.Services.AppService;
using CarCheckup.Domain.Services.Service;
using CarCheckup.Infra.EfCore.Common;
using CarCheckup.Infra.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection("SiteSettings").Get<SiteSettings>();
if (siteSettings != null)
    builder.Services.AddSingleton(siteSettings);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<CarCheckupDbContext>(options => options.UseSqlServer(siteSettings is null ? "Data Source=WIN10\\SQLEXPRESS;Initial Catalog=CarCheckupDb;Integrated Security=True;Trust Server Certificate=True":siteSettings.ConnectionStrings.SqlConnection));
builder.Services.AddScoped<ICarModelAppService, CarModelAppService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICarModelRepository, CarModelRepository>();
builder.Services.AddScoped<ICheckupRequestAppService, CheckupRequestAppService>();
builder.Services.AddScoped<ICheckupRequestService, CheckupRequestService>();
builder.Services.AddScoped<ICheckupRequestRepository, CheckupRequestRepository>();
builder.Services.AddScoped<ICarAppService, CarAppService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRejectedCheckupRequestService, RejectedCheckupRequestService>();
builder.Services.AddScoped<IRejectedCheckupRequestRepository, RejectedCheckupRequestRepository>();
//builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); //
    //app.UseSwagger();  // فعال کردن Swagger
    //app.UseSwaggerUI();  // فعال کردن رابط Swagger UI

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
