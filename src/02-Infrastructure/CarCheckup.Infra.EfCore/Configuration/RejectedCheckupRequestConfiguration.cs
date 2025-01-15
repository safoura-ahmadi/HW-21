using CarCheckup.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCheckup.Infra.EfCore.Configuration;

public class RejectedCheckupRequestConfiguration : IEntityTypeConfiguration<RejectedCheckupRequest>
{
    public void Configure(EntityTypeBuilder<RejectedCheckupRequest> builder)
    {
        builder.HasOne(rcr => rcr.Car)
            .WithMany(c => c.RejectedCheckupRequests)
            .HasForeignKey(rcr => rcr.CarId);
    }
}
