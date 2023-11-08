using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;
public interface IPermissionRepository : IBaseRepository<Permission>
{
    bool CheckExistsPermission(string permissionName);
    bool RemovePermission(long Id);
}
