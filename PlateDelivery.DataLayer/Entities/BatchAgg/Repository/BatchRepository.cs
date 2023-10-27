using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.BatchAgg.Repository;

internal class BatchRepository : BaseRepository<Batch>, IBatchRepository
{
    public BatchRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}