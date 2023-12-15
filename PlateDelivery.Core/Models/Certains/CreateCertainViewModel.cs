using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Certains;
public class CreateCertainViewModel : BaseDto
{
    [Required]
    public string CertainName { get; set; }
    [Required]
    public string CertainCode { get; set; } 
    public CertainCategory Category { get; set; }
}
