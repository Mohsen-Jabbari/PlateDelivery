using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.Core.Services.Accounts;
using PlateDelivery.Core.Services.Provinces;

namespace PlateDelivery.Web.Pages.Leon.Accounts
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public CreateAndEditAccountViewModel CreateAccountViewModel { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_accountService.IsAccountExist(CreateAccountViewModel.BankCode))
            {
                ModelState.AddModelError("CreateAccountViewModel.BankCode", "بانک با این کد قبلا در سیستم ثبت شده است");
                return Page();
            }

            long id = _accountService.CreateAccount(CreateAccountViewModel);
            return RedirectToPage("Index");
        }
    }
}