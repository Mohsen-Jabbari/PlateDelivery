using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;

internal class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public bool CheckExistsPermission(string permissionName)
    {
        if (Context.Permissions.Where(p => p.PermissionName == permissionName).Any())
            return true;
        return false;
    }

    public bool RemovePermission(long Id)
    {
        var permission = Context.Permissions.Find(Id);
        if (permission == null) 
            return false;
        Context.Permissions.Remove(permission);
        Context.SaveChanges();
        return true;
    }
}
