using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Services.ServiceCodings;

namespace PlateDelivery.Web.Pages.Leon.ServiceCodings
{
    //[UserRoleChecker]
    //[PermissionChecker(29)]
    public class IndexModel : PageModel
    {
        private readonly IServiceCodingService _serviceCodingService;

        public IndexModel(IServiceCodingService serviceCodingService)
        {
            _serviceCodingService = serviceCodingService;
        }

        public ServiceCodingsViewModel ServiceCodingsViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50, string filterByServiceName = "", string filterByServiceCode = "")
        {
            ViewData["Title"] = "کدینگ مبلغ و خدمت";

            if (Request.Query.ContainsKey("fsn"))
            {
                string s = Request.Query["fsn"];
                if (s != "")
                    filterByServiceName = Request.Query["fsn"];
            }

            if (Request.Query.ContainsKey("fsc"))
            {
                string s = Request.Query["fsc"];
                if (s != "")
                    filterByServiceCode = Request.Query["fsc"];
            }

            ViewData["filterServiceName"] = filterByServiceName;
            ViewData["filterServiceCode"] = filterByServiceCode;

            ViewData["PageID"] = (pageId - 1) * take + 1;
            ServiceCodingsViewModel = _serviceCodingService.GetServiceCodings(pageId, take, filterByServiceName, filterByServiceCode);

            if (pageId > 1 && pageId != ServiceCodingsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == ServiceCodingsViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (ServiceCodingsViewModel.ServiceCodingsCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}