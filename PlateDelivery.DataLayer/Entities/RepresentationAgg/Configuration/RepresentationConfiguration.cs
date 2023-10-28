using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.RepresentationAgg.Configuration;
internal class RepresentationConfiguration : IEntityTypeConfiguration<Representation>
{
    public void Configure(EntityTypeBuilder<Representation> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.RepresentationCode)
            .IsRequired();

        builder.Property(b => b.BrokerCode)
            .IsRequired();
    }
}
