using PlateDelivery.DataLayer.Entities.AccountAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Certains;
public class CreateAndEditCertainViewModel : BaseDto
{
    [Display(Name = "عنوان معین")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string CertainName { get; set; }
    [Display(Name = "کد معین")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string CertainCode { get; set; }
    [Display(Name = "گروه معین")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public CertainCategory Category { get; set; }
}

public class CertainsViewModel
{
    public List<Certain>  Certains { get; set; }
    public int CertainsCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}
