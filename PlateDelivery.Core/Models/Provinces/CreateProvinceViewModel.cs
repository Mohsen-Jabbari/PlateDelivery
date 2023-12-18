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
    [Required]
    public string ProvinceName { get; set; }
    [Required]
    public string SubProvince { get; set; }
    [Required]
    public string ProvinceCode { get; set; }
}

public class EditProvinceViewModel : BaseDto
{
    [Required]
    public string ProvinceName { get; set; }
    [Required]
    public string SubProvince { get; set; }
    [Required]
    public string ProvinceCode { get; set; }
}
