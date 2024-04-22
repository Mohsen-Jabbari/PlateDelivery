using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class ExceptTopYarTmpModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;

        public ExceptTopYarTmpModel(ITopYarTmpService topYarTmpService)
        {
            _topYarTmpService = topYarTmpService;
        }

        public CreateAndEditTopYarTmpViewModel model { get; set; }
        public void OnGet(long Id)
        {
            ViewData["Title"] = "تایید رکورد تاپ یار";
            model = _topYarTmpService.GetById(Id);
        }

        public IActionResult OnPost(long Id)
        {
            _topYarTmpService.ExceptTopYarTmpRecord(Id);
            return RedirectToPage("Index");
        }
    }
}