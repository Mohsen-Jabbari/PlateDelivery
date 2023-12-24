using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Representations;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Representations;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class DeteleTmpModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;

        public DeteleTmpModel(ITopYarTmpService topYarTmpService)
        {
            _topYarTmpService = topYarTmpService;
        }

        public TopYarTmpViewModel TopYarTmpViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 50)
        {
            ViewData["Title"] = "حذف دیتای تاپ یار";
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps(pageId, take);
        }

        public IActionResult OnPost()
        {
            _topYarTmpService.DeleteTopYarTmp();
            return RedirectToPage("Index");
        }
    }
}