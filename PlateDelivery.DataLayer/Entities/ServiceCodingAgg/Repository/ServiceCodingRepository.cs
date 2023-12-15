using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;

internal class ServiceCodingRepository : BaseRepository<ServiceCoding>, IServiceCodingRepository
{
    public ServiceCodingRepository(PlateDeliveryDBContext context) : base(context)
    {

    }

    public void DeleteServiceCoding(long Id)
    {
        throw new NotImplementedException();
    }
}