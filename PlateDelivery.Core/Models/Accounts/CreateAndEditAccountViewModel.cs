using PlateDelivery.DataLayer.Entities.AccountAgg;
using PlateDelivery.DataLayer.Entities.ProvinceAgg;
using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Accounts;
public class CreateAndEditAccountViewModel : BaseDto
{
    [Display(Name = "شماره شبا")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Iban { get; set; }
    [Display(Name = "کد بانک")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string BankCode { get; set; }
    [Display(Name = "نام بانک")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string BankName { get; set; }
}

public class AccountsViewModel
{
    public List<Account> Accounts { get; set; }
    public int AccountsCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}
