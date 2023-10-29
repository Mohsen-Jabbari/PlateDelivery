using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.DataLayer.Context;
using PlateDelivery.DataLayer.Entities.BatchAgg.Repository;
using PlateDelivery.DataLayer.Entities.CounterAgg.Repository;
using PlateDelivery.DataLayer.Entities.OkAgg.Repository;
using PlateDelivery.DataLayer.Entities.RepresentationAgg.Repository;
using PlateDelivery.DataLayer.Entities.StoreAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.DataLayer;
public static class DataLayerBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IStoreRepository, StoreRepository>();
        services.AddTransient<IRepresentationRepository, RepresentationRepository>();
        services.AddTransient<IOkRepository, OkRepository>();
        services.AddTransient<ICounterRepository, CounterRepository>();
        services.AddTransient<IBatchRepository, BatchRepository>();

        services.AddTransient(_ => new DapperContext.DapperContext(connectionString));
        services.AddDbContext<PlateDeliveryDBContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
