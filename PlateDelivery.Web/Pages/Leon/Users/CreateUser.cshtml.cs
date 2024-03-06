using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Security;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    [UserRoleChecker]
    [PermissionChecker(2, 1)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public CreateUserModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _roleService.GetRoles();
        }

        public IActionResult OnPost(List<long> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _roleService.GetRoles();
                return Page();
            }

            if (_userService.IsUserNameExitsts(CreateUserViewModel.UserName))
            {
                ViewData["Roles"] = _roleService.GetRoles();
                ModelState.AddModelError("CreateUserViewModel.UserName", "نام کاربری قبلا در سیستم ثبت شده است");
                return Page();
            }

            long userId = _userService.CreateUser(CreateUserViewModel);
            _userService.AddRolesToUser(selectedRoles, userId);

            return RedirectToPage("Index");
        }
    }
}