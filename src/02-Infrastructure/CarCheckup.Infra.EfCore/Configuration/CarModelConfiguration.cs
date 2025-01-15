using CarCheckup.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCheckup.Infra.EfCore.Configuration;

public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.Property(cm => cm.Name).IsRequired()
             .HasMaxLength(55).HasColumnType("nvarchar");

        builder.HasMany(cm => cm.Cars)
            .WithOne(c => c.Model)
            .HasForeignKey(c => c.ModelId);

        builder.HasData
        (

           new CarModel()
           {
               Id = 1,
               Name = "پژو206"
           },
              new CarModel()
              {
                  Id = 2,
                  Name = "سمند"
              },
                 new CarModel()
                 {
                     Id = 3,
                     Name = "پراید"
                 }

        );

    }
}
