using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.Users;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Web.Pages.Leon.Representations
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class RestoreRepresentationModel : PageModel
    {
        private IRepresentationService _representationService;

        public RestoreRepresentationModel(IRepresentationService representationService)
        {
            _representationService = representationService;
        }

        public EditRepresentationViewModel model { get; set; }
        public void OnGet(long id)
        {
            model = _representationService.GetById(id);
            ViewData["id"] = id;
        }

        public IActionResult OnPost(long id)
        {
            _representationService.RestoreRepresentation(id);
            return RedirectToPage("ListDeleteRepresentations");
        }
    }
}