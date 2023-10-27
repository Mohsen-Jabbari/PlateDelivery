using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.StoreAgg.Repository;

internal class StoreRepository : BaseRepository<Store>, IStoreRepository
{
    public StoreRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}
