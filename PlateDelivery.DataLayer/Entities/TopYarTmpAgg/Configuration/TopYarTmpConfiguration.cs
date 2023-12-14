using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Configuration;
internal class TopYarTmpConfiguration : IEntityTypeConfiguration<TopYarTmp>
{
    public void Configure(EntityTypeBuilder<TopYarTmp> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.RetrivalRef);

        builder.HasIndex(b => b.Terminal);

        builder.HasIndex(b => b.ServiceCode);

        builder.HasIndex(b => b.TrackingNo);

        builder.HasIndex(b => b.TransactionDate);

        builder.Property(b => b.RetrivalRef)
            .IsRequired();

        builder.Property(b => b.Terminal)
            .IsRequired();

        builder.Property(b => b.ServiceCode)
            .IsRequired();

        builder.Property(b => b.TrackingNo)
            .IsRequired();

        builder.Property(b => b.TransactionDate)
            .IsRequired();
    }
}
