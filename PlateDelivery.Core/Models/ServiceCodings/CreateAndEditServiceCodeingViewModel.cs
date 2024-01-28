using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.ServiceCodings;
public class CreateAndEditServiceCodeingViewModel : BaseDto
{
    [Display(Name = "نام خدمت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ServiceName { get; set; }
    [Display(Name = "نام کامل خدمت")]
    public string ServiceFullName { get; set; }
    [Display(Name = "کد خدمت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ServiceCode { get; set; }
    [Display(Name = "کد معین درآمد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public long CertainId { get; set; }
    [Display(Name = "کد تفضیل سطح 4")]
    public string? CodeLevel4 { get; set; }
    [Display(Name = "کد تفضیل سطح 6")]
    public string? CodeLevel6 { get; set; }

    [Display(Name = "مبلغ خدمت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public long Amount { get; set; }

    [Display(Name = "مالیات ارزش افزوده")]
    public bool IncludeTax { get; set; }
}

public class ServiceCodingsViewModel
{
    public List<ServiceCoding> ServiceCodings { get; set; }
    public int ServiceCodingsCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}