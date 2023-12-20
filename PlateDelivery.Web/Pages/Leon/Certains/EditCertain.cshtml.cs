using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.Certains;
using PlateDelivery.Core.Services.Certains;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

namespace PlateDelivery.Web.Pages.Leon.Certains
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 9)]
    public class EditCertainModel : PageModel
    {
        private readonly ICertainService _certainService;

        public EditCertainModel(ICertainService certainService)
        {
            _certainService = certainService;
        }

        [BindProperty]
        public CreateAndEditCertainViewModel EditCertainViewModel { get; set; }

        public IActionResult OnGet(int id)
        {
            EditCertainViewModel = _certainService.GetById(id);
            ViewData["Category"] = EditCertainViewModel.Category.GetSelectList();
            return Page();
        }


        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
            {
                EditCertainViewModel = _certainService.GetById(id);
                ViewData["Category"] = EditCertainViewModel.Category.GetSelectList();
                return Page();
            }

            var editCertainResult = _certainService.EditCertain(EditCertainViewModel);
            if (!editCertainResult)
            {
                EditCertainViewModel = _certainService.GetById(id);
                ViewData["Category"] = EditCertainViewModel.Category.GetSelectList();
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}