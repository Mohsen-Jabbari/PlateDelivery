using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Configuration;
internal class ServiceCodingConfiguration : IEntityTypeConfiguration<ServiceCoding>
{
    public void Configure(EntityTypeBuilder<ServiceCoding> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.ServiceName);

        builder.HasIndex(b => b.ServiceFullName);

        builder.HasIndex(b => b.ServiceCode);

        builder.Property(b => b.ServiceName)
            .IsRequired();

        builder.Property(b => b.ServiceFullName)
            .IsRequired();

        builder.Property(b => b.ServiceCode)
            .IsRequired();
    }
}
