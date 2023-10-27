using Microsoft.EntityFrameworkCore;

namespace PlateDelivery.DataLayer.Context;
public class PlateDeliveryDBContext : DbContext
{
    public PlateDeliveryDBContext(DbContextOptions<PlateDeliveryDBContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlateDeliveryDBContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
