using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.Roles;
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
    }
}
