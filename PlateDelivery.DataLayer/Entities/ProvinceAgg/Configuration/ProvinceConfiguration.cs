using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.ProvinceAgg.Configuration;
internal class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.ProvinceName);

        builder.HasIndex(b => b.SubProvince);

        builder.HasIndex(b => b.ProvinceCode);

        builder.Property(b => b.ProvinceName)
            .IsRequired();

        builder.Property(b => b.SubProvince)
            .IsRequired();

        builder.Property(b => b.ProvinceCode)
            .IsRequired();
    }
}
