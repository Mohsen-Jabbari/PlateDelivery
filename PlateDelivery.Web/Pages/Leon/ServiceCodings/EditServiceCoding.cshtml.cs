using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Services.ServiceCodings;

namespace PlateDelivery.Web.Pages.Leon.ServiceCodings
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditServiceCodingModel : PageModel
    {
        private readonly IServiceCodingService _serviceCodingService;

        public EditServiceCodingModel(IServiceCodingService serviceCodingService)
                                                        => _serviceCodingService = serviceCodingService;

        [BindProperty]
        public CreateAndEditServiceCodeingViewModel EditServiceCodingViewModel { get; set; }

        public IActionResult OnGet(int id)
        {
            EditServiceCodingViewModel = _serviceCodingService.GetById(id);
            ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditServiceCodingViewModel = _serviceCodingService.GetById(id);
                ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
                return Page();
            }

            var editServiceCodingResult = _serviceCodingService.EditServiceCoding(EditServiceCodingViewModel);
            if (!editServiceCodingResult)
            {
                EditServiceCodingViewModel = _serviceCodingService.GetById(id);
                ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}