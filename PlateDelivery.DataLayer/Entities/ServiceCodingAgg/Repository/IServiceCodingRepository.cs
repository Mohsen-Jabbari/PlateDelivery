using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;
public interface IServiceCodingRepository : IBaseRepository<ServiceCoding>
{
    void DeleteServiceCoding(long Id);
}
