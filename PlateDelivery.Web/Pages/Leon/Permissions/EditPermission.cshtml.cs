using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Services.Permissions;

namespace PlateDelivery.Web.Pages.Leon.Permissions

{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 25)]
    public class EditPermissionModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public EditPermissionModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public PermissionViewModel Permission { get; set; }
        public IActionResult OnGet(long Id)
        {
            Permission = _permissionService.GetPermission(Id);
            if (Permission == null)
                return RedirectToPage("Index");
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            return Page();
        }

        public IActionResult OnPost(long selectedParent)
        {
            if (!ModelState.IsValid)
                return Page();

            if (selectedParent == 0)
            {
                bool result = _permissionService.EditPermission(Permission.Id, Permission.PermissionName, null);
                return RedirectToPage("Index");
            }
            else
            {
                bool result = _permissionService.EditPermission(Permission.Id, Permission.PermissionName, selectedParent);
                return RedirectToPage("Index");
            }

        }
    }
}