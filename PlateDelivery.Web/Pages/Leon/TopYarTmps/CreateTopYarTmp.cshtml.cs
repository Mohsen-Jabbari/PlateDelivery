using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateTopYarTmpModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;

        public CreateTopYarTmpModel(ITopYarTmpService topYarTmpService)
                                                    => _topYarTmpService = topYarTmpService;

        [BindProperty]
        public CreateAndEditTopYarTmpViewModel CreateTopYarTmpViewModel { get; set; }

        public void OnGet()
        {
            CreateTopYarTmpViewModel = new CreateAndEditTopYarTmpViewModel() { ProvinceCode = "0" };
            ViewData["Title"] = "ایجاد دیتای تاپ یار";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "ایجاد دیتای تاپ یار";
                return Page();
            }

            if (_topYarTmpService.IsTopYarTmpExist(CreateTopYarTmpViewModel.RetrivalRef))
            {
                ModelState.AddModelError("CreateTopYarTmpViewModel.RetrivalRef", "این شماره مرجع قبلا در سیستم ثبت شده است");
                ViewData["Title"] = "ایجاد دیتای تاپ یار";
                return Page();
            }

            long id = _topYarTmpService.CreateTopYarTmp(CreateTopYarTmpViewModel);
            return RedirectToPage("Index");
        }
    }
}