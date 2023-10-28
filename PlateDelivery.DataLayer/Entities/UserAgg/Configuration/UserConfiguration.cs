using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.UserAgg.Configuration;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.UserName)
            .IsRequired();

        builder.Property(b => b.Password)
            .IsRequired();
    }
}
