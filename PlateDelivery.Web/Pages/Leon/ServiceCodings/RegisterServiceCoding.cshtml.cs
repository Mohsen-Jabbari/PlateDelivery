using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Services.Certains;
using PlateDelivery.Core.Services.ServiceCodings;

namespace PlateDelivery.Web.Pages.Leon.ServiceCodings
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class RegisterServiceCodingModel : PageModel
    {
        private readonly IServiceCodingService _serviceCodingService;
        private readonly ICertainService _certainService;

        public RegisterServiceCodingModel(IServiceCodingService serviceCodingService, ICertainService certainService)
        {
            _serviceCodingService = serviceCodingService;
            _certainService = certainService;
        }

        [BindProperty]
        public CreateAndEditServiceCodeingViewModel CreateServiceCodeingViewModel { get; set; }

        public void OnGet(string ServiceName, string ServiceCode)
        {
            CreateServiceCodeingViewModel = new();
            CreateServiceCodeingViewModel.ServiceCode = ServiceCode;
            CreateServiceCodeingViewModel.ServiceName = ServiceName;
            ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
            var groups = _certainService.GetIncomeCertain();
            ViewData["Category"] = new SelectList(groups, "Id", "Text");
        }

        public IActionResult OnPost(long category)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
                var groups = _certainService.GetIncomeCertain();
                ViewData["Category"] = new SelectList(groups, "Id", "Text");
                return Page();
            }

            if (_serviceCodingService
                .IsServiceCodingExist(CreateServiceCodeingViewModel.ServiceCode, CreateServiceCodeingViewModel.CodeLevel4))
            {
                ModelState.AddModelError("CreateServiceCodeingViewModel.ServiceCode", "این مبلغ و خدمت قبلا در سیستم ثبت شده است");
                ViewData["Title"] = "ایجاد کد مبلغ و خدمت";
                var groups = _certainService.GetIncomeCertain();
                ViewData["Category"] = new SelectList(groups, "Id", "Text");
                return Page();
            }
            CreateServiceCodeingViewModel.CertainId = category;
            long id = _serviceCodingService.CreateServiceCoding(CreateServiceCodeingViewModel);
            return RedirectToPage("TopYarTmps/Index");
        }
    }
}