using Microsoft.EntityFrameworkCore;
using PlateDelivery.DataLayer.Entities.AccountAgg;
using PlateDelivery.DataLayer.Entities.BatchAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.CounterAgg;
using PlateDelivery.DataLayer.Entities.OkAgg;
using PlateDelivery.DataLayer.Entities.PermissionAgg;
using PlateDelivery.DataLayer.Entities.ProvinceAgg;
using PlateDelivery.DataLayer.Entities.RepresentationAgg;
using PlateDelivery.DataLayer.Entities.RoleAgg;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.StoreAgg;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.DataLayer.Context;
public class PlateDeliveryDBContext : DbContext
{
    public PlateDeliveryDBContext(DbContextOptions<PlateDeliveryDBContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Representation> Representations { get; set; }
    public DbSet<Ok> Oks { get; set; }
    public DbSet<Counter> Counters { get; set; }
    public DbSet<Batch> Batches { get; set; }


    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public DbSet<Certain> Certains { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<ServiceCoding> ServiceCodings { get; set; }

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
