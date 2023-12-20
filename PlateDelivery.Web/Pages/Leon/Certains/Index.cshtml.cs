using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Certains;
using PlateDelivery.Core.Services.Certains;

namespace PlateDelivery.Web.Pages.Leon.Certains
{
    //[UserRoleChecker]
    //[PermissionChecker(29)]
    public class IndexModel : PageModel
    {
        private readonly ICertainService _certainService;

        public IndexModel(ICertainService certainService)
        {
            _certainService = certainService;
        }

        public CertainsViewModel CertainsViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50, string filterByCertainCode = "")
        {
            if (Request.Query.ContainsKey("fc"))
            {
                string s = Request.Query["fc"];
                if (s != "")
                    filterByCertainCode = Request.Query["fc"];
            }

            ViewData["FilterCertainCode"] = filterByCertainCode;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            CertainsViewModel = _certainService.GetCertains(pageId, take, filterByCertainCode);

            if (pageId > 1 && pageId != CertainsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == CertainsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (CertainsViewModel.CertainsCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}