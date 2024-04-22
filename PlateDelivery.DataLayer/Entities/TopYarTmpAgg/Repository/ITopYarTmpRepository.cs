using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;
public interface ITopYarTmpRepository : IBaseRepository<TopYarTmp>
{
    bool DeleteTopYarTmp(long Id);
    bool ExceptTopYarTmp(long Id);
    bool DeleteTopYarTmp(List<long> Ids);
    void DeleteAllTopYarTmp();
    void DeleteUnUsedTopYarTmp(List<string> UnUsedAccount);
    TopYarTmp GetTopYarTmpFirstRecord();
}
