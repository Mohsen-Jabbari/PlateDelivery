using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Representations
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 11)]
    public class ListDeleteRepresentationsModel : PageModel
    {
        private IRepresentationService _representationsService;

        public ListDeleteRepresentationsModel(IRepresentationService representationsService)
        {
            _representationsService = representationsService;
        }

        public RepresentationViewModel RepresentationViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 5, string filterByName = "", string filterByBrokerCode = "")
        {
            if (Request.Query.ContainsKey("fn"))
            {
                string? s = Request.Query["fn"];
                if (s != "")
                    filterByName = Request.Query["fn"];
            }

            if (Request.Query.ContainsKey("fb"))
            {
                string? s = Request.Query["fb"];
                if (s != "")
                    filterByBrokerCode = Request.Query["fb"];
            }

            ViewData["FilterName"] = filterByName;
            ViewData["FilterBrokerCode"] = filterByBrokerCode;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            RepresentationViewModel = _representationsService.GetDeletedRepresentations(pageId, take, filterByName, filterByBrokerCode);

            if (pageId > 1 && pageId != RepresentationViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == RepresentationViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (RepresentationViewModel.RepresentationCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}