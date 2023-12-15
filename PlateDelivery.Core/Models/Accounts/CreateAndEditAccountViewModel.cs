using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Accounts;
public class CreateAndEditAccountViewModel : BaseDto
{
    [Required]
    public string Iban { get; set; }
    [Required]
    public string BankCode { get; set; }
    [Required]
    public string BankName { get; set; }
}
