using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.CertainAgg.Configuration;
internal class CertainConfiguration : IEntityTypeConfiguration<Certain>
{
    public void Configure(EntityTypeBuilder<Certain> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.CertainName);

        builder.HasIndex(b => b.CertainCode);

        builder.Property(b => b.CertainName)
            .IsRequired();

        builder.Property(b => b.CertainCode)
            .IsRequired();
    }
}
