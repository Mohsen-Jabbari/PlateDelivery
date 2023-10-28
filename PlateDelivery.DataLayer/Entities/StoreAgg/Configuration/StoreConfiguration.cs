using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.StoreAgg.Configuration;
internal class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.PlateText)
            .IsRequired();

        builder.Property(b => b.PlateNumber)
            .IsRequired();

        builder.Property(b => b.OperationType)
            .IsRequired();
    }
}