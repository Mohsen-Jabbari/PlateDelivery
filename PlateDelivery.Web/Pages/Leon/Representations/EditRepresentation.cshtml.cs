using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Services.Representations;

namespace PlateDelivery.Web.Pages.Leon.Representations
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditRepresentationModel : PageModel
    {
        private readonly IRepresentationService _representationService;

        public EditRepresentationModel(IRepresentationService representationService)
        {
            _representationService = representationService;
        }

        [BindProperty]
        public EditRepresentationViewModel EditRepresentationViewModel { get; set; }
        public IActionResult OnGet(int id)
        {
            EditRepresentationViewModel = _representationService.GetById(id);
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditRepresentationViewModel = _representationService.GetById(id);
                return Page();
            }

            var editUserResult = _representationService.EditRepresentation(EditRepresentationViewModel);
            if (!editUserResult)
            {
                EditRepresentationViewModel = _representationService.GetById(id);
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}