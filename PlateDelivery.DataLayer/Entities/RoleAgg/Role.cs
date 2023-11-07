using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.RoleAgg;
public class Role : BaseEntity
{
    public string RoleName { get; private set; }
    public bool IsDelete { get; private set; } = false;
    public List<RolePermission> RolePermissions { get; private set; }

    public Role(string roleName)
    {
        Guard(roleName);
        RoleName = roleName;
        RolePermissions = new();
    }

    public void Edit(string roleName)
    {
        Guard(roleName);
        RoleName = roleName;
    }

    public void Delete()
    {
        IsDelete = true;
    }

    #region RolePermission
    public void SetPermissions(List<RolePermission> rolePermissions)
    {
        RolePermissions = rolePermissions;
    }
    #endregion

    public static void Guard(string roleName)
    {
        NullOrEmptyDataException.CheckString(roleName, nameof(roleName));
    }
}
