using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.Core.Security;

public class UserRoleCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private IPermissionService _permissionService;
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _permissionService =
            (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var userId = context.HttpContext.User.GetUserId();
            if (!_permissionService.CheckUserIsRole(userId))
            {
                context.Result = new RedirectResult("/Login?permission=false");
            }
        }
        else
        {
            context.Result = new RedirectResult("/Login");
        }
    }
}