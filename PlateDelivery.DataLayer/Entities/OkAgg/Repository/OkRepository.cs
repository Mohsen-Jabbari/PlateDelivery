using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.OkAgg.Repository;

internal class OkRepository : BaseRepository<Ok>, IOkRepository
{
    public OkRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}