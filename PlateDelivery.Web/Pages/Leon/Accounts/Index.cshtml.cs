using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Services.Accounts;

namespace PlateDelivery.Web.Pages.Leon.Accounts
{
    //[UserRoleChecker]
    //[PermissionChecker(29)]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public AccountsViewModel AccountsViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50, string filterByBankCode = "")
        {
            if (Request.Query.ContainsKey("fb"))
            {
                string s = Request.Query["fb"];
                if (s != "")
                    filterByBankCode = Request.Query["fb"];
            }

            ViewData["FilterBankCode"] = filterByBankCode;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            AccountsViewModel = _accountService.GetAccounts(pageId, take, filterByBankCode);

            if (pageId > 1 && pageId != AccountsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == AccountsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (AccountsViewModel.AccountsCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}