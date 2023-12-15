using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;

internal class TopYarTmpRepository : BaseRepository<TopYarTmp>, ITopYarTmpRepository
{
    public TopYarTmpRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public void DeleteTopYarTmp(long Id)
    {
        throw new NotImplementedException();
    }
}