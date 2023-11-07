using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.DataLayer.Entities.RoleAgg.Configuration;
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(b => b.Id).IsUnique();

        builder.Property(b => b.RoleName)
            .IsRequired();

        builder.OwnsMany(b => b.RolePermissions, option =>
        {
            option.ToTable("RolePermissions", "dbo");
        });
    }
}