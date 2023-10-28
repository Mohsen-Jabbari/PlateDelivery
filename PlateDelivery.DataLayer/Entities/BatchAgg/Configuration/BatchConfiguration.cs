using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.BatchAgg.Configuration;
internal class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.ChassisTrim);

        builder.HasIndex(b => b.ContractOwnerId);

        builder.Property(b => b.ChassisTrim)
            .IsRequired();

        builder.Property(b => b.AgencyPlaceId)
            .IsRequired();

        builder.Property(b => b.ContractOwnerId)
            .IsRequired();
    }
}
