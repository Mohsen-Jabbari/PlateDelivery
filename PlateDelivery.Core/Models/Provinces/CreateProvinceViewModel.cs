using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Provinces;
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
