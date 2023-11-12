using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.Users;

public class ChangePasswordViewModel
{

    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
    public string OldPassword { get; set; }

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
    [Compare("Password", ErrorMessage = "کلمه عبور مغایرت دارد")]
    public string RePassword { get; set; }
}