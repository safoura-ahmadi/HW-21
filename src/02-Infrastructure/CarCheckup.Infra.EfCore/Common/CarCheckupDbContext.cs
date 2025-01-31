
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarCheckup.Infra.EfCore.Common;

public class CarCheckupDbContext : IdentityDbContext<Operator, IdentityRole<int>, int>
{
    public CarCheckupDbContext(DbContextOptions<CarCheckupDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new CarConfiguration());
        //modelBuilder.ApplyConfiguration(new CarModelConfiguration());
        //modelBuilder.ApplyConfiguration(new CheckupRequestConfiguration());
        //modelBuilder.ApplyConfiguration(new RejectedCheckupRequestConfiguration());
    }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CheckupRequest> CheckupRequests { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
     public DbSet<RejectedCheckupRequest> RejectedCheckupRequests { get; set; }

}
