using CarCheckup.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCheckup.Infra.EfCore.Configuration;

public class CheckupRequestConfiguration : IEntityTypeConfiguration<CheckupRequest>
{
    public void Configure(EntityTypeBuilder<CheckupRequest> builder)
    {
        builder.Property(cr => cr.TimeToDone)
            .IsRequired()
            .HasColumnType("date");

        builder.HasOne(cr => cr.Car)
            .WithMany(c => c.CheckupRequests)
            .HasForeignKey(c => c.CarId);
    }
}
