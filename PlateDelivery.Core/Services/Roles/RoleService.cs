using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.RoleAgg;
using PlateDelivery.DataLayer.Entities.RoleAgg.Repository;

namespace PlateDelivery.Core.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public long AddRole(string roleName)
    {
        if (!_roleRepository.Exists(u => u.RoleName == roleName))
        {
            var role = new Role(roleName);
            _roleRepository.Add(role);
            _roleRepository.SaveSync();
            return role.Id;
        }
        return -1;
    }

    public bool EditRole(long Id, string roleName)
    {
        var role = _roleRepository.GetTrackingSync(Id);
        if (role != null)
        {
            role.Edit(roleName);
            _roleRepository.SaveSync();
            return true;
        }
        return false;
    }

    public bool DeleteRole(long roleId)
    {
        var role = _roleRepository.GetTrackingSync(roleId);
        if (role != null)
        {
            _roleRepository.RemoveRole(roleId);
            return true;
        }
        return false;
    }

    

    public RoleViewModel GetRoleById(int roleId)
    {
        var role = _roleRepository.GetTrackingSync(roleId);
        if (role != null)
            return new RoleViewModel()
            {
                CreationDate = role.CreationDate,
                RoleName = role.RoleName,
                Id = role.Id
            };
        return null;
    }

    public List<Role> GetRoles()
    {
        return _roleRepository.GetAll();
    }
}