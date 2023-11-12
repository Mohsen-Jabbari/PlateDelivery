using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public EditUserModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        public IActionResult OnGet(int id)
        {
            ViewData["Roles"] = _roleService.GetRoles();
            EditUserViewModel = _userService.GetUserByIdForEdit(id);
            return Page();
        }


        public IActionResult OnPost(List<long> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _roleService.GetRoles();
                EditUserViewModel = _userService.GetUserByIdForEdit(EditUserViewModel.Id);
                return Page();
            }

            var editUserResult = _userService.EditUser(EditUserViewModel);
            if (!editUserResult)
            {
                ViewData["Roles"] = _roleService.GetRoles();
                EditUserViewModel = _userService.GetUserByIdForEdit(EditUserViewModel.Id);
                return Page();
            }

            var editRoleResult = _userService.EditRolesUser(selectedRoles, EditUserViewModel.Id);
            if (!editRoleResult)
            {
                ViewData["Roles"] = _roleService.GetRoles();
                EditUserViewModel = _userService.GetUserByIdForEdit(EditUserViewModel.Id);
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}