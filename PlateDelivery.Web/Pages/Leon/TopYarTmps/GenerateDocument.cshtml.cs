using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.ServiceCodings;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class GenerateDocumentModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;
        private readonly IServiceCodingService _serviceCodingService;

        public GenerateDocumentModel(ITopYarTmpService topYarTmpService, IServiceCodingService serviceCodingService)
        {
            _topYarTmpService = topYarTmpService;
            _serviceCodingService = serviceCodingService;
        }

        public TopYarTmpViewModel TopYarTmpViewModel { get; set; }
        public ServiceCodingsViewModel ServiceCodingsViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "ایجاد سند حسابداری";
            ServiceCodingsViewModel = _serviceCodingService.GetServiceCodings();
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmpsForDocument();
        }

        public IActionResult OnPost()
        {
            _topYarTmpService.DeleteTopYarTmp();
            return RedirectToPage("Index");
        }
    }
}