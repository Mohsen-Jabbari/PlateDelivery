using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.RoleAgg.Repository;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}