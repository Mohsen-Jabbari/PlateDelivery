using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel InformationUserViewModel { get; set; }
        public IActionResult OnGet(int UserId)
        {
            InformationUserViewModel = _userService.GetUserInformation(UserId);
            long currentUser = User.GetUserId();
            if (currentUser == UserId)
                return RedirectToPage("Index");
            ViewData["UserId"] = UserId;
            return Page();
        }

        public IActionResult OnPost(int UserId)
        {
            long currentUser = User.GetUserId();
            if (currentUser == UserId)
                return RedirectToPage("Index");
            _userService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}