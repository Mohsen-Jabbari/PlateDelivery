using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg;
public class Permission : BaseEntity
{
    public Permission(string permissionName)
    {
        Guard(permissionName);
        PermissionName = permissionName;
    }

    public void Edit(string permissionName)
    {
        Guard(permissionName);
        PermissionName = permissionName;
    }

    public string PermissionName { get; private set; }

    public static void Guard(string permissionName)
    {
        NullOrEmptyDataException.CheckString(permissionName, nameof(permissionName));
    }

}
