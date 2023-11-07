using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.DataLayer.Entities.RoleAgg;
public class RolePermission : BaseEntity
{
    public RolePermission(long roleId, long permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public long RoleId { get; internal set; }
    public long PermissionId { get; private set; }
}
