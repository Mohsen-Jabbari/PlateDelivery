using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace LatinMedia.Web.Pages.Leon.Roles
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 24)]
    public class IndexModel : PageModel
    {
        private IRoleService _roleService;

        public IndexModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public List<Role> RolesList { get; set; }
        public void OnGet()
        {
            RolesList = _roleService.GetRoles();
        }
    }
}