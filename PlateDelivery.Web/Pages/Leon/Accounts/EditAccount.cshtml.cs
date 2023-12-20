using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Services.Accounts;

namespace PlateDelivery.Web.Pages.Leon.Accounts
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditProvinceModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditProvinceModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public CreateAndEditAccountViewModel EditAccountViewModel { get; set; }
        public IActionResult OnGet(int id)
        {
            EditAccountViewModel = _accountService.GetById(id);
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditAccountViewModel = _accountService.GetById(id);
                return Page();
            }

            var editAccountResult = _accountService.EditAccount(EditAccountViewModel);
            if (!editAccountResult)
            {
                EditAccountViewModel = _accountService.GetById(id);
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}