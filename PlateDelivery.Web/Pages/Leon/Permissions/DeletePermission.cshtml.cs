using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Services.Permissions;

namespace PlateDelivery.Web.Pages.Leon.Permissions
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1 }, 27)]
    public class DeletePermissionModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public DeletePermissionModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public PermissionViewModel Permission { get; set; }
        public IActionResult OnGet(int id)
        {
            Permission = _permissionService.GetPermission(id);
            if (Permission == null)
                return RedirectToPage("Index");
            return Page();
        }

        public IActionResult OnPost()
        {
            _permissionService.DeletePermission(Permission.Id);
            return RedirectToPage("Index");
        }
    }
}