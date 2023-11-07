using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.PermissionAgg.Repository;

internal class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}
