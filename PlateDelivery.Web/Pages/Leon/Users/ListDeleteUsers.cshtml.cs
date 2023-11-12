using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Users
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 11)]
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UsersViewModel UsersViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 5, string filterByLastName = "", string filterByUserName = "")
        {
            if (Request.Query.ContainsKey("fl"))
            {
                string s = Request.Query["fl"];
                if (s != "")
                    filterByLastName = Request.Query["fl"];
            }

            if (Request.Query.ContainsKey("fu"))
            {
                string s = Request.Query["fu"];
                if (s != "")
                    filterByUserName = Request.Query["fu"];
            }

            ViewData["FilterLastName"] = filterByLastName;
            ViewData["FilterUserName"] = filterByUserName;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            UsersViewModel = _userService.GetDeleteUsers(pageId, take, filterByLastName, filterByUserName);

            if (pageId > 1 && pageId != UsersViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == UsersViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (UsersViewModel.UserCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}