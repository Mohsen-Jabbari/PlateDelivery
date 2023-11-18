using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.Roles;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.Pages.Leon.Representations
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateRepresentationModel : PageModel
    {
        private readonly IRepresentationService _representationService;

        public CreateRepresentationModel(IRepresentationService representationService)
        {
            _representationService = representationService;
        }

        [BindProperty]
        public CreateRepresentationViewModel model { get; set; }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_representationService.IsRepresentationCodeExists(model.RepresentationCode))
            {
                ModelState.AddModelError("model.RepresentationCode", "نمایندگی قبلا در سیستم ثبت شده است");
                return Page();
            }

            long id = _representationService.CreateRepresentation(model);
            return RedirectToPage("Index");
        }
    }
}