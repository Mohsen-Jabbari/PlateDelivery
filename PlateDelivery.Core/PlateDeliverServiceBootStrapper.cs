using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Services.Accounts;
using PlateDelivery.Core.Services.Certains;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Provinces;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.Core.Services.ServiceCodings;
using PlateDelivery.Core.Services.TopYarTmps;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Core;
public static class PlateDeliverServiceBootStrapper
{
    public static void InitServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IViewRenderService, RenderViewToString>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRepresentationService, RepresentationService>();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<ICertainService, CertainService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IServiceCodingService, ServiceCodingService>();
        services.AddScoped<ITopYarTmpService, TopYarTmpService>();
    }
}
