using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.DataLayer.Entities.OkAgg.Repository;

namespace PlateDelivery.Web.Pages.Leon.ImportData
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 2)]
    public class IndexModel : PageModel
    {
        private readonly IOkRepository _repository;

        public IndexModel(IOkRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost() 
        {
            var result = _repository.ImportOkFile();
            return Page();
        }
    }
}