using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Configuration;
internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();
        
        builder.HasIndex(b => b.Order);

        builder.HasIndex(b => b.Year);

        builder.HasIndex(b => b.Month);

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
