using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 2 }, 5)]
    public class UserResetPassModel : PageModel
    {
        private IUserService _userService;

        public UserResetPassModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public void OnGet(int UserId)
        {
            ViewData["UserId"] = UserId;

        }

        public IActionResult OnPost(int UserId)
        {
            if (!ModelState.IsValid)
                return Page();


            _userService.ChangePasswordViewModel(UserId, ChangePasswordViewModel.Password);
            ViewData["CssClass"] = "alert alert-success";
            ViewData["Message"] = "کلمه عبور با موفقیت به روز رسانی شد";

            return RedirectToPage("Index");
        }
    }
}