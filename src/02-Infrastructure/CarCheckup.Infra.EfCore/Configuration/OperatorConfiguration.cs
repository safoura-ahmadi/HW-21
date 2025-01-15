using CarCheckup.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCheckup.Infra.EfCore.Configuration;

public class OperatorConfiguration : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        builder.Property(o => o.Username).IsRequired()
            .HasMaxLength(20)
            .IsFixedLength()
           .HasColumnType("varchar");

        builder.Property(o => o.Password).IsRequired()
            .HasMaxLength(6)
            .IsFixedLength()
             .HasColumnType("varchar");
        builder.HasData(

            new Operator()
            {
                Id = 1,
                Username = "operator",
                Password = "123456"
            }


            );
    }
}
