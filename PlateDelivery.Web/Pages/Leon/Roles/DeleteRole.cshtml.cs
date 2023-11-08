using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace PlateDelivery.Web.Pages.Leon.Roles
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 27)]
    public class DeleteRoleModel : PageModel
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService, IRoleService roleService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _roleService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _roleService.DeleteRole(Role.Id);
            return RedirectToPage("Index");
        }
    }
}