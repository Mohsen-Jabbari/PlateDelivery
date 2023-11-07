using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Models;
public class SideBarAdminPanelViewModel
{
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AvatarName { get; set; }
    public List<UserRoleDto> RollId { get; set; }
}

public class UsersViewModel
{
    public List<User> Users { get; set; }
    public int UserCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int LastPage { get; set; }
    public int PrevPage { get; set; }
    public int NextPage { get; set; }
}

public class UserRoleDto
{
    public int UR_Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
