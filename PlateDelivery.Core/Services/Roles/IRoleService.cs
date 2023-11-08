using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace PlateDelivery.Core.Services.Roles;
public interface IRoleService
{
    #region Role

    long AddRole(string roleName);
    bool EditRole(long Id, string roleName);
    bool DeleteRole(long roleId);
    


    List<Role> GetRoles();
    RoleViewModel GetRoleById(int roleId);
    #endregion
}
