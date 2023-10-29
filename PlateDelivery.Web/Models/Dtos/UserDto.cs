namespace PlateDelivery.Web.Models.Dtos;
public class UserDto : BaseDto
{
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDelete { get; private set; }
}

