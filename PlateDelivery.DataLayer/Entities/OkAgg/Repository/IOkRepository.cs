using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.OkAgg.Repository;
public interface IOkRepository : IBaseRepository<Ok>
{
    bool ImportOkFile();
}
