using CarCheckup.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCheckup.Infra.EfCore.Configuration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(c => c.Plate)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(10)
            .IsFixedLength();

        builder.Property(c => c.GenerationYear)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(c => c.OwnerMeliCode)
            .IsRequired()
            .HasMaxLength(10)
            .IsFixedLength()
            .HasColumnType("varchar");

        builder.Property(c => c.OwnerMobile)
            .IsRequired()
            .HasMaxLength(13)
            .HasColumnType("varchar");


        builder.HasData
        (

            new Car()
            {
                Id = 1,
                OwnerMeliCode = "0312889161",
                OwnerMobile = "09302675549",
                Company = Domain.Core.Enums.Car.CarCompanyEnum.IranKhodro,
                ModelId = 2,
                GenerationYear = new(2020, 1, 1),
                Plate = "33و283-68"

            },

            new Car()
            {
                Id = 2,
                OwnerMeliCode = "0322879161",
                OwnerMobile = "09303685549",
                Company = Domain.Core.Enums.Car.CarCompanyEnum.Saipa,
                ModelId = 3,
                GenerationYear = new(2025, 1, 1),
                Plate = "99خ902-33",

            },

            new Car()
            {
                Id = 3,
                OwnerMeliCode = "0312889161",
                OwnerMobile = "09302675549",
                Company = Domain.Core.Enums.Car.CarCompanyEnum.IranKhodro,
                ModelId = 1,
                GenerationYear = new(2015, 1, 1),
                Plate = "45ر444-41"

            }


        );
    }
}
