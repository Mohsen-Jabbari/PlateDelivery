using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.Core.Services.Provinces;

namespace PlateDelivery.Web.Pages.Leon.Provinces
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditProvinceModel : PageModel
    {
        private readonly IProvinceService _provinceService;

        public EditProvinceModel(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [BindProperty]
        public EditProvinceViewModel EditProvinceViewModel { get; set; }
        public IActionResult OnGet(int id)
        {
            EditProvinceViewModel = _provinceService.GetById(id);
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditProvinceViewModel = _provinceService.GetById(id);
                return Page();
            }

            var editProvinceResult = _provinceService.EditProvince(EditProvinceViewModel);
            if (!editProvinceResult)
            {
                EditProvinceViewModel = _provinceService.GetById(id);
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}