using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.Certains;
using PlateDelivery.Core.Services.Certains;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using PlateDelivery.Core.Convertors;

namespace PlateDelivery.Web.Pages.Leon.Certains
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 8)]
    public class CreateCertainModel : PageModel
    {
        private readonly ICertainService _certainService;

        public CreateCertainModel(ICertainService certainService)
        {
            _certainService = certainService;
        }

        [BindProperty]
        public CreateAndEditCertainViewModel CreateCertainViewModel { get; set; }

        [BindProperty]
        public CertainCategory CertainCategory { get; set; }
        public void OnGet()
        {
            ViewData["Category"] = CertainCategory.GetSelectList();
        }

        public IActionResult OnPost(CertainCategory category)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = CertainCategory.GetSelectList();
                return Page();
            }

            if (_certainService.IsCertainExist(CreateCertainViewModel.CertainCode))
            {
                ModelState.AddModelError("CreateCertainViewModel.CertainCode", "این کد معین قبلا در سیستم ثبت شده است");
                ViewData["Category"] = CertainCategory.GetSelectList();
                return Page();
            }

            CreateCertainViewModel.Category = category;
            long id = _certainService.CreateCertain(CreateCertainViewModel);
            return RedirectToPage("Index");
        }
    }
}