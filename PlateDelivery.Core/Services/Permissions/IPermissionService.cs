using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.PermissionAgg;
using PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;
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
    List<long> RolePermissions(long roleId);
    void EditRolePermissions(long roleId, List<long> permissions);
    bool CheckPermission(long permissionId, string mobile);
    //bool CheckUserIsRole(string mobile);
    //bool IsInRole(int UID, int RoleID);
}


internal class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
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
        throw new NotImplementedException();
    }

    public bool CheckPermission(long permissionId, string mobile)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}