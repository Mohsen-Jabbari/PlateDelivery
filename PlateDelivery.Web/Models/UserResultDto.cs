namespace PlateDelivery.Web.Models
{
    public class UserResultDto : BaseDto
    {
        public string? Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? UserId { get; set; }
        public string? RefreshToken { get; set; }
    }
}
