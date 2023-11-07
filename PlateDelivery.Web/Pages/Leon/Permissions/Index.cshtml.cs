using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.DataLayer.Entities.PermissionAgg;

namespace LatinMedia.Web.Pages.Leon.Permissions
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 24)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Permission> PermissionsList { get; set; }
        public void OnGet()
        {
            PermissionsList = _permissionService.GetAllPermissions();
        }
    }
}