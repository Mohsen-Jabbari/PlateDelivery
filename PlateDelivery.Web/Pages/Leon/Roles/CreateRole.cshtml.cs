using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace PlateDelivery.Web.Pages.Leon.Roles
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 25)]
    public class CreateRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;

        public CreateRoleModel(IPermissionService permissionService, IRoleService roleService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
        }

        [BindProperty]
        public RoleViewModel Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(List<long> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            long roleId = _roleService.AddRole(Role.RoleName);
            _permissionService.AddPermissionsToRole(roleId, selectedPermission);
            return RedirectToPage("Index");
        }
    }
}