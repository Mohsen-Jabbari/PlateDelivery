using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.DataLayer.Entities.UserAgg;

public class UserRole : BaseEntity
{
    public UserRole(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public long UserId { get; internal set; }
    public long RoleId { get; private set; }
}