namespace PlateDelivery.Core.Models;
public class UserDto : BaseDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
}

