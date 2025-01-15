
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Common;

public class CarCheckupDbContext(DbContextOptions options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new CarModelConfiguration());
        modelBuilder.ApplyConfiguration(new CheckupRequestConfiguration());
        modelBuilder.ApplyConfiguration(new OperatorConfiguration());
        modelBuilder.ApplyConfiguration(new RejectedCheckupRequestConfiguration());
    }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CheckupRequest> CheckupRequests { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Operator> Operators { get; set; }
    public DbSet<RejectedCheckupRequest> RejectedCheckupRequests { get; set; }

}
