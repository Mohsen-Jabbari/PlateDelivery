using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.Core.Services.Users;
using PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;
using System.Security;

namespace PlateDelivery.Core;
public static class PlateDeliverServiceBootStrapper
{
    public static void InitServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IViewRenderService, RenderViewToString>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService,PermissionService>();
    }
}
