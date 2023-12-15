using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.ServiceCodings;
public class CreateAndEditServiceCodeingViewModel : BaseDto
{
    [Required]
    public string ServiceName { get; set; }
    public string ServiceFullName { get; set; }
    [Required]
    public string ServiceCode { get; set; }
    [Required]
    public string CodeLevel4 { get; set; }
    [Required]
    public string CodeLevel6 { get; set; }
}
