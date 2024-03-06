using Microsoft.EntityFrameworkCore;
using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.PermissionAgg;
using PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;
using PlateDelivery.DataLayer.Entities.RoleAgg;
using PlateDelivery.DataLayer.Entities.RoleAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;
using System.Reflection;

namespace PlateDelivery.Core.Services.Permissions;

internal class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public PermissionService(IPermissionRepository permissionRepository, IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public long AddPermission(string permissionName, long? parentId)
    {
        if (!_permissionRepository.CheckExistsPermission(permissionName))
        {
            var permission = new Permission(permissionName, parentId);
            _permissionRepository.Add(permission);
            _permissionRepository.SaveSync();
            return permission.Id;
        }
        return -1;
    }

    public bool EditPermission(long id, string permissionName, long? parentId)
    {
        var permission = _permissionRepository.GetTrackingSync(id);
        if (permission == null)
            return false;
        permission.Edit(permissionName, parentId);
        _permissionRepository.SaveSync();
        return true;
    }

    public bool DeletePermission(long permissionId)
    {
        var permission = _permissionRepository.GetTrackingSync(permissionId);
        if (permission == null)
            return false;

        return _permissionRepository.RemovePermission(permissionId);
    }

    public void AddPermissionsToRole(long roleId, List<long> permissions)
    {
        var role = _roleRepository.GetTrackingSync(roleId);
        if (role != null)
        {
            role.RolePermissions.Clear();
            var rolePermission = new List<RolePermission>();
            foreach (var perm in permissions)
                rolePermission.Add(new RolePermission(roleId, perm));
            role.RolePermissions.AddRange(rolePermission);
            _roleRepository.SaveSync();
        }
    }

    public bool UpdatePermissionsRole(long roleId, List<long> permissions)
    {
        var role = _roleRepository.GetTrackingSync(roleId);
        if (role != null)
        {
            var rolePermission = new List<RolePermission>();
            role.RolePermissions.Clear();
            foreach (var permission in permissions)
                rolePermission.Add(new RolePermission(roleId, permission));
            role.RolePermissions.AddRange(rolePermission);
            _roleRepository.SaveSync();
            return true;
        }
        return false;
    }

    public bool CheckPermission(long roleId, long permissionId, long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user != null)
        {
            var UserRole = user.UserRoles
            .Where(u => u.UserId == userId && u.RoleId == roleId).Select(u => u.RoleId).FirstOrDefault();
            if (UserRole < 0)
                return false;

            var Role = _roleRepository.GetTrackingSync(roleId);
            if (Role != null)
            {
                var RolesPermission = Role.RolePermissions
                .Where(p => p.PermissionId == permissionId && p.RoleId == UserRole).Select(p => p.RoleId).ToList();
                if (!RolesPermission.Any())
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public bool CheckUserIsRole(long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user != null)
        {
            bool userRoles = user.UserRoles.Any();
            if (userRoles)
                return true;
            else
                return false;
        }
        return false;
    }



    public void EditRolePermissions(long roleId, List<long> permissions)
    {
        throw new NotImplementedException();
    }

    public List<PermissionViewModel> GetAllPermissions()
    {
        var permissions = _permissionRepository.GetAll();
        if (permissions == null)
            return null;
        var result = new List<PermissionViewModel>();
        foreach (var permission in permissions)
            result.Add(new PermissionViewModel()
            {
                Id = permission.Id,
                CreationDate = permission.CreationDate,
                ParentId = permission.ParentId,
                PermissionName = permission.PermissionName
            });
        return result;
    }

    public PermissionViewModel GetPermission(long id)
    {
        var permission = _permissionRepository.GetTrackingSync(id);
        if (permission == null)
            return null;
        return new PermissionViewModel()
        {
            Id = id,
            CreationDate = permission.CreationDate,
            ParentId = permission.ParentId,
            PermissionName = permission.PermissionName
        };
    }

    public List<long> RolePermissions(long roleId)
    {
        var role = _roleRepository.GetTrackingSync(roleId);
        if (role == null)
            return null;
        List<long> permissions = new List<long>();
        foreach (var permission in role.RolePermissions)
            permissions.Add(permission.PermissionId);
        return permissions;
    }


}