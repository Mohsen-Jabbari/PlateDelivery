using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Services.ServiceCodings;

namespace PlateDelivery.Web.Pages.Leon.ServiceCodings
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateServiceCodingModel : PageModel
    {
        private readonly IServiceCodingService _serviceCodingService;

        public CreateServiceCodingModel(IServiceCodingService serviceCodingService) 
                                                    => _serviceCodingService = serviceCodingService;

        [BindProperty]
        public CreateAndEditServiceCodeingViewModel CreateServiceCodeingViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
                return Page();
            }

            if (_serviceCodingService.IsServiceCodingExist(CreateServiceCodeingViewModel.ServiceCode))
            {
                ModelState.AddModelError("CreateServiceCodeingView.ServiceCode", "این مبلغ و خدمت قبلا در سیستم ثبت شده است");
                ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
                return Page();
            }

            long id = _serviceCodingService.CreateServiceCoding(CreateServiceCodeingViewModel);
            return RedirectToPage("Index");
        }
    }
}