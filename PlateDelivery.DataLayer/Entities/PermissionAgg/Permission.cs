using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg;
public class Permission : BaseEntity
{
    public Permission(string permissionName, long? parentId)
    {
        Guard(permissionName);
        PermissionName = permissionName;
        ParentId = parentId;
    }

    public void Edit(string permissionName, long? parentId)
    {
        Guard(permissionName);
        ParentId = parentId;
        PermissionName = permissionName;
    }

    public string PermissionName { get; private set; }
    public long? ParentId { get; private set; }

    public static void Guard(string permissionName)
    {
        NullOrEmptyDataException.CheckString(permissionName, nameof(permissionName));
    }

}
