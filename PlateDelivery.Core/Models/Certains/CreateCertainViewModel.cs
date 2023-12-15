using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Certains;
public class CreateCertainViewModel : BaseDto
{
    [Required]
    public string CertainName { get; private set; }
    [Required]
    public string CertainCode { get; private set; } 
    public CertainCategory Category { get; private set; }
}
