﻿using Microsoft.Extensions.DependencyInjection;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Core;
public static class PlateDeliverServiceBootStrapper
{
    public static void InitServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IViewRenderService, RenderViewToString>();
    }
}