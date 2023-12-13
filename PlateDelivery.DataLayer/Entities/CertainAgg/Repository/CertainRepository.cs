using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.CertainAgg.Repository;

internal class CertainRepository : BaseRepository<Certain>, ICertainRepository
{
    public CertainRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}