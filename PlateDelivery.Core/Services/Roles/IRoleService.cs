using PlateDelivery.DataLayer.Entities.RoleAgg;

namespace PlateDelivery.Core.Services.Roles
{
    public interface IRoleService
    {
        #region Role

        void AddRole(Role role);
        void EditRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<long> roleIds, int userId);
        void EditRolesUser(int userId, List<long> rolesId);
        void RemoveUserRoles(int userId);

        

        List<Role> GetRoles();
        int GetUserRoleById(int Id);
        List<string> GetUserRoles(int userId);
        Role GetRoleById(int roleId);
        #endregion
    }
}
