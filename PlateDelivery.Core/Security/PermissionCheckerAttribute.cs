using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.Core.Security;

public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly long _permissionId;
    private readonly long _rollId;
    private IPermissionService _permissionService;

    public PermissionCheckerAttribute(long permissionId, long rollId)
    {
        _permissionId = permissionId;
        _rollId = rollId;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _permissionService =
            (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            long userId = context.HttpContext.User.GetUserId();
            if (!_permissionService.CheckPermission(_rollId, _permissionId, userId))
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