﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class RestoreUserModel : PageModel
    {
        private IUserService _userService;

        public RestoreUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel InformationUserViewModel { get; set; }
        public void OnGet(int UserId)
        {
            InformationUserViewModel = _userService.GetUserInformation(UserId);
            ViewData["UserId"] = UserId;
        }

        public IActionResult OnPost(int UserId)
        {
            _userService.RestoreUser(UserId);
            return RedirectToPage("Index");
        }
    }
}