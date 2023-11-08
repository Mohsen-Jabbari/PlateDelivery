using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Services.Permissions;
using PlateDelivery.DataLayer.Entities.PermissionAgg;

namespace PlateDelivery.Web.Pages.Leon.Permissions

{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 25)]
    public class CreatePermissionModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public CreatePermissionModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public PermissionViewModel Permission { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(long selectedParent)
        {
            if (!ModelState.IsValid)
                return Page();

            if (selectedParent == 0)
            {
                long permissionId = _permissionService.AddPermission(Permission.PermissionName, null);
                return RedirectToPage("Index");
            }
            else
            {
                long permissionId = _permissionService.AddPermission(Permission.PermissionName, selectedParent);
                return RedirectToPage("Index");
            }
            
        }
    }
}