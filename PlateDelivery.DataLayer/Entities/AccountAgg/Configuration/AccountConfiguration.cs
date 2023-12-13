using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlateDelivery.DataLayer.Entities.AccountAgg.Configuration;
internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.HasIndex(b => b.Iban);

        builder.HasIndex(b => b.BankCode);

        builder.HasIndex(b => b.BankName);

        builder.Property(b => b.Iban)
            .IsRequired();

        builder.Property(b => b.BankCode)
            .IsRequired();

        builder.Property(b => b.BankName)
            .IsRequired();
    }
}
