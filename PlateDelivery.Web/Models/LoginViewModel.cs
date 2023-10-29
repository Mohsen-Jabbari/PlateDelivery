using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Web.Models
{
    public class LoginViewModel
    {
        public long UserId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string? Password { get; set; }

        public string? UnHashedPassword { get; set; }

        [Display(Name = "کد امنیتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(4, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string? CaptchaCode { get; set; }

    }
}
