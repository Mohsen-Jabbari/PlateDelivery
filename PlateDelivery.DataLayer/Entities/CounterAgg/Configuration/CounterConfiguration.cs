using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.CounterAgg.Configuration;
internal class CounterConfiguration : IEntityTypeConfiguration<Counter>
{
    public void Configure(EntityTypeBuilder<Counter> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.PlateCount)
            .IsRequired();
    }
}
