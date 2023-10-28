using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.OkAgg.Configuration;
internal class OkConfiguration : IEntityTypeConfiguration<Ok>
{
    public void Configure(EntityTypeBuilder<Ok> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.PlateNumber);

        builder.HasIndex(b => b.ChassisTrim);

        builder.Property(b => b.InvoiceNumber)
            .IsRequired();

        builder.Property(b => b.ChassisNumber)
            .IsRequired();

        builder.Property(b => b.PlateNumber)
            .IsRequired();
    }
}
