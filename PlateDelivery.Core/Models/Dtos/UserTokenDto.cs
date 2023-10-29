namespace PlateDelivery.Core.Models;

public class UserTokenDto : BaseDto
{
    public long UserId { get; set; }
    public string HashJwtToken { get; set; }
    public string HashRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }
}

public class UserTokenResponseDto : BaseDto
{
    public string? Token { get; set; }
    public DateTime ExpireDate { get; set; }
    public string? UserId { get; set; }
    public string? RefreshToken { get; set; }
}

