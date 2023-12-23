using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Services.Certains;
using PlateDelivery.Core.Services.ServiceCodings;

namespace PlateDelivery.Web.Pages.Leon.ServiceCodings
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditServiceCodingModel : PageModel
    {
        private readonly IServiceCodingService _serviceCodingService;
        private readonly ICertainService _certainService;

        public EditServiceCodingModel(IServiceCodingService serviceCodingService, ICertainService certainService)
        {
            _serviceCodingService = serviceCodingService;
            _certainService = certainService;
        }

        [BindProperty]
        public CreateAndEditServiceCodeingViewModel EditServiceCodingViewModel { get; set; }

        public IActionResult OnGet(int id)
        {
            EditServiceCodingViewModel = _serviceCodingService.GetById(id);
            var groups = _certainService.GetIncomeCertain();
            ViewData["Category"] = new SelectList(groups, "Id", "Text");
            ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditServiceCodingViewModel = _serviceCodingService.GetById(id);
                var groups = _certainService.GetIncomeCertain();
                ViewData["Category"] = new SelectList(groups, "Id", "Text");
                ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
                return Page();
            }

            var editServiceCodingResult = _serviceCodingService.EditServiceCoding(EditServiceCodingViewModel);
            if (!editServiceCodingResult)
            {
                EditServiceCodingViewModel = _serviceCodingService.GetById(id);
                var groups = _certainService.GetIncomeCertain();
                ViewData["Category"] = new SelectList(groups, "Id", "Text");
                ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}