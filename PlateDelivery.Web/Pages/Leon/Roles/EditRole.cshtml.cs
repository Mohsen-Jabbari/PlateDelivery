using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace PlateDelivery.Web.Pages.Leon.Roles
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 26)]
    public class EditRoleModel : PageModel
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService, IRoleService roleService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _roleService.GetRoleById(id);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            //ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(id);
        }

        public IActionResult OnPost(List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            _roleService.EditRole(Role.Id,Role.RoleName);
            //_roleService.UpdatePermissionsRole(Role.RoleId, selectedPermission);
            return RedirectToPage("Index");
        }
    }
}