using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.Core.Services.Provinces;

namespace PlateDelivery.Web.Pages.Leon.Provinces
{
    //[UserRoleChecker]
    //[PermissionChecker(29)]
    public class IndexModel : PageModel
    {
        private readonly IProvinceService _provinceService;

        public IndexModel(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public ProvincesViewModel ProvincesViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50, string filterByProvince = "")
        {
            if (Request.Query.ContainsKey("fp"))
            {
                string s = Request.Query["fp"];
                if (s != "")
                    filterByProvince = Request.Query["fp"];
            }

            ViewData["FilterProvince"] = filterByProvince;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            ProvincesViewModel = _provinceService.GetProvinces(pageId, take, filterByProvince);

            if (pageId > 1 && pageId != ProvincesViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == ProvincesViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (ProvincesViewModel.ProvincesCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}