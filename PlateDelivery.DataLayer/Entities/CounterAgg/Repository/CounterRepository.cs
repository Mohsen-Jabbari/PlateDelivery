using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.CounterAgg.Repository;

internal class CounterRepository : BaseRepository<Counter>, ICounterRepository
{
    public CounterRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}