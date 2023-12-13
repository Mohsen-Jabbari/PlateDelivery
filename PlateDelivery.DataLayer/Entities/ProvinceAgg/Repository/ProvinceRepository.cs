using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;

internal class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}