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

        builder.HasIndex(b => b.UserName).IsUnique();

        builder.Property(b => b.Password)
            .IsRequired();

        builder.OwnsMany(b => b.Tokens, option =>
        {
            option.ToTable("Tokens", "dbo");
            option.HasKey(b => b.Id);

            option.Property(b => b.HashJwtToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.HashRefreshToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.Device)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}
