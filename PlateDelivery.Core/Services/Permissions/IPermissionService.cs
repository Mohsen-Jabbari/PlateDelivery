using PlateDelivery.DataLayer.Entities.PermissionAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Core.Services.Permissions;
public interface IPermissionService
{
    void AddPermission(Permission permission);
    void EditPermission(Permission permission);
    void DeletePermission(long PermissionId);
    List<Permission> GetAllPermissions();
    void AddPermissionsToRole(int roleId, List<int> permissions);
    List<int> RolePermissions(int roleId);
    void EditRolePermissions(int roleId, List<int> permissions);
    bool CheckPermission(int permissionId, string mobile);
    //bool CheckUserIsRole(string mobile);
    //bool IsInRole(int UID, int RoleID);
}
