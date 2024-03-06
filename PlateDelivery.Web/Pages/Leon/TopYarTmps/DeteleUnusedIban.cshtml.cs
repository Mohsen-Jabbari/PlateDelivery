using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Accounts;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class DeteleUnusedIbanModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;
        private readonly IAccountService _accountService;

        public DeteleUnusedIbanModel(ITopYarTmpService topYarTmpService, IAccountService accountService)
        {
            _topYarTmpService = topYarTmpService;
            _accountService = accountService;
        }

        public TopYarTmpViewModel TopYarTmpViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50)
        {
            ViewData["Title"] = "حذف رکوردهای نامرتبط با موسسه";
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps();
            //پیدا کردن شماره شبا های نامرتبط با موسسه
            if (TopYarTmpViewModel.ProvinceMessage == null)
            {
                var accounts = _accountService.GetAccounts(1, 50, "").Accounts.Select(a => a.Iban.Replace("\r\n", "")).ToList();
                var unUsedAccount = TopYarTmpViewModel.TopYarTmps.Where(t => !accounts.Contains(t.Iban)).Select(t => t.Iban).Distinct().ToList();
                if (unUsedAccount.Count > 0)
                {
                    ViewData["UnUsedRecords"] = TopYarTmpViewModel.TopYarTmps.Where(t => unUsedAccount.Contains(t.Iban)).Count();
                    ViewData["UsedRecords"] = TopYarTmpViewModel.TopYarTmps.Where(t => !unUsedAccount.Contains(t.Iban)).Count();
                }
            }
        }

        public IActionResult OnPost()
        {
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps();
            var accounts = _accountService.GetAccounts(1, 50, "").Accounts.Select(a => a.Iban.Replace("\r\n", "")).ToList();
            var unUsedAccount = TopYarTmpViewModel.TopYarTmps.Where(t => !accounts.Contains(t.Iban)).Select(t => t.Iban).Distinct().ToList();
            _topYarTmpService.DeleteUnUsedRecords(unUsedAccount);
            return RedirectToPage("Index");
        }
    }
}