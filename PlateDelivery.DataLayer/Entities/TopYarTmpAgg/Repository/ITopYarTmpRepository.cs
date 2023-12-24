using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;
public interface ITopYarTmpRepository : IBaseRepository<TopYarTmp>
{
    void DeleteTopYarTmp(long Id);
    void DeleteAllTopYarTmp();
    void DeleteUnUsedTopYarTmp(List<string> UnUsedAccount);
}
