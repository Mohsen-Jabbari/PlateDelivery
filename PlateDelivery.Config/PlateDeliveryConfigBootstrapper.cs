using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.Core;
using PlateDelivery.DataLayer;

namespace PlateDelivery.Config;
public static class PlateDeliveryConfigBootstrapper
{
    public static void RegisterPlateDeliveryDependency(this IServiceCollection services, string connectionString)
    {
        DataLayerBootstrapper.Init(services, connectionString);

        services.InitServiceDependency();
    }
}
