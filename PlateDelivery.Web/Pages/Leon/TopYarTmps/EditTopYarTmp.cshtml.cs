using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditTopYarTmpModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;

        public EditTopYarTmpModel(ITopYarTmpService topYarTmpService)
                                                        => _topYarTmpService = topYarTmpService;

        [BindProperty]
        public CreateAndEditTopYarTmpViewModel EditTopYarTmpViewModel { get; set; }

        public IActionResult OnGet(int id)
        {
            EditTopYarTmpViewModel = _topYarTmpService.GetById(id);
            ViewData["Title"] = "ویرایش دیتای تاپ یار";
            return Page();
        }


        public IActionResult OnPost(long id, string txtProvince, long hfProvinceId)
        {
            string[] subs = txtProvince.Split(" - ");
            EditTopYarTmpViewModel.ProvinceName = subs[0];
            EditTopYarTmpViewModel.ProvinceCode = hfProvinceId.ToString();
            if (!ModelState.IsValid)
            {
                EditTopYarTmpViewModel = _topYarTmpService.GetById(id);
                ViewData["Title"] = "ویرایش دیتای تاپ یار";
                return Page();
            }

            var editTopYarTmpResult = _topYarTmpService.EditTopyarTmp(EditTopYarTmpViewModel);
            if (!editTopYarTmpResult)
            {
                EditTopYarTmpViewModel = _topYarTmpService.GetById(id);
                ViewData["Title"] = "ویرایش کد مبلغ و خدمت";
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}