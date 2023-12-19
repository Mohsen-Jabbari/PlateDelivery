using PlateDelivery.DataLayer.Entities.ProvinceAgg;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Provinces;

public class ProvincesViewModel
{
    public List<Province> Provinces { get; set; }
    public int ProvincesCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}


public class CreateProvinceViewModel : BaseDto
{
    [Display(Name = "نام استان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ProvinceName { get; set; }
    [Display(Name = "نام شهر یا مرکز")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string SubProvince { get; set; }
    [Display(Name = "کد استان یا مرکز")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ProvinceCode { get; set; }
}

public class EditProvinceViewModel : BaseDto
{
    [Display(Name = "نام استان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ProvinceName { get; set; }
    [Display(Name = "نام شهر یا مرکز")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string SubProvince { get; set; }
    [Display(Name = "کد استان یا مرکز")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ProvinceCode { get; set; }
}
