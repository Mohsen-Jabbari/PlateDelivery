namespace PlateDelivery.Core.Models;

public class UserTokenDto : BaseDto
{
    public string? Token { get; set; }
    public DateTime ExpireDate { get; set; }
    public string? UserId { get; set; }
    public string? RefreshToken { get; set; }
}

