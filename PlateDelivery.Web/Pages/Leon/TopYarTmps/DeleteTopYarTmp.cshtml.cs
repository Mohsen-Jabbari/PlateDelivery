using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class DeleteTopYarTmpModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;

        public DeleteTopYarTmpModel(ITopYarTmpService topYarTmpService)
        {
            _topYarTmpService = topYarTmpService;
        }

        public CreateAndEditTopYarTmpViewModel model { get; set; }
        public void OnGet(long Id)
        {
            ViewData["Title"] = "حذف دیتای تاپ یار";
            model = _topYarTmpService.GetById(Id);
        }

        public IActionResult OnPost(long Id)
        {
            _topYarTmpService.DeleteTopYarTmpRecord(Id);
            return RedirectToPage("Index");
        }
    }
}