using PlateDelivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Core.Services.Permissions;
public interface IPermissionService
{
    long AddPermission(string permissionName, long? parentId);
    bool EditPermission(long id, string permissionName, long? parentId);
    bool DeletePermission(long permissionId);


    List<PermissionViewModel> GetAllPermissions();
    PermissionViewModel GetPermission(long id);


    void AddPermissionsToRole(long roleId, List<long> permissions);
    bool UpdatePermissionsRole(long roleId, List<long> permissions);


    List<long> RolePermissions(long roleId);
    void EditRolePermissions(long roleId, List<long> permissions);
    bool CheckPermission(long permissionId, string mobile);
}
