using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg.Configuration;
internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.PermissionName)
            .IsRequired();
    }
}
