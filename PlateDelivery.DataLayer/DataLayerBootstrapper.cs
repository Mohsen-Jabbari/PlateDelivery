using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.DataLayer.Context;
using PlateDelivery.DataLayer.Entities.AccountAgg.Repository;
using PlateDelivery.DataLayer.Entities.CertainAgg.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;
using PlateDelivery.DataLayer.Entities.OkAgg.Repository;
using PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;
using PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;
using PlateDelivery.DataLayer.Entities.RepresentationAgg.Repository;
using PlateDelivery.DataLayer.Entities.RoleAgg.Repository;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.DataLayer;
public static class DataLayerBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRepresentationRepository, RepresentationRepository>();
        services.AddTransient<IOkRepository, OkRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<ICertainRepository, CertainRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IProvinceRepository, ProvinceRepository>();
        services.AddTransient<IServiceCodingRepository, ServiceCodingRepository>();
        services.AddTransient<ITopYarTmpRepository, TopYarTmpRepository>();
        services.AddTransient<IDocumentRepository, DocumentRepository>();

        services.AddTransient(_ => new DapperContext.DapperContext(connectionString));
        services.AddDbContext<PlateDeliveryDBContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
