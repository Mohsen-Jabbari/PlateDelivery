namespace PlateDelivery.Core.Models.Users;

public class EditUserViewModel : BaseDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public List<long> UserRoles { get; set; } = new List<long>();
}
