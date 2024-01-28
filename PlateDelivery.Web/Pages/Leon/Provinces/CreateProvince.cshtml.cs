using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.Core.Services.Provinces;

namespace PlateDelivery.Web.Pages.Leon.Provinces
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateProvinceModel : PageModel
    {
        private readonly IProvinceService _provinceService;

        public CreateProvinceModel(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [BindProperty]
        public CreateProvinceViewModel CreateProvinceViewModel { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_provinceService.IsProvinceExist(CreateProvinceViewModel.ProvinceCode, CreateProvinceViewModel.CodeLevel4))
            {
                ModelState.AddModelError("CreateProvinceViewModel.ProvinceCode", "استان با این کد قبلا در سیستم ثبت شده است");
                return Page();
            }

            long id = _provinceService.CreateProvince(CreateProvinceViewModel);
            return RedirectToPage("Index");
        }
    }
}