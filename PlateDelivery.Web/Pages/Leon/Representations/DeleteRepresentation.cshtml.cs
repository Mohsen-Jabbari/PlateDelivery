using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Services.Representations;

namespace PlateDelivery.Web.Pages.Leon.Representations
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class DeleteRepresentationModel : PageModel
    {
        private IRepresentationService _representationService;

        public DeleteRepresentationModel(IRepresentationService representationService)
        {
            _representationService = representationService;
        }

        public EditRepresentationViewModel model { get; set; }
        public IActionResult OnGet(long id)
        {
            model = _representationService.GetById(id);
            return Page();
        }

        public IActionResult OnPost(long id)
        {
            _representationService.DeleteRepresentation(id);
            return RedirectToPage("Index");
        }
    }
}