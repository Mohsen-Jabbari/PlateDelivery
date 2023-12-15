using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.CertainAgg.Repository;
public interface ICertainRepository : IBaseRepository<Certain>
{
    void DeleteCertain(long Id);
}
