using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;

internal class TopYarTmpRepository : BaseRepository<TopYarTmp>, ITopYarTmpRepository
{
    public TopYarTmpRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public void DeleteAllTopYarTmp()
    {
        var results = Context.TopYarTmps.ToList();
        Context.TopYarTmps.RemoveRange(results);
        Context.SaveChanges();
    }

    public bool DeleteTopYarTmp(long Id)
    {
        var item = Context.TopYarTmps.FirstOrDefault(x => x.Id == Id);
        if (item != null)
        {
            Context.TopYarTmps.Remove(item);
            Context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteTopYarTmp(List<long> Ids)
    {
        var items = Context.TopYarTmps.Where(x => Ids.Contains(x.Id)).ToList();
        if (items != null)
        {
            Context.TopYarTmps.RemoveRange(items);
            Context.SaveChanges();
            return true;
        }
        return false;
    }

    public void DeleteUnUsedTopYarTmp(List<string> UnUsedAccount)
    {
        var results = Context.TopYarTmps.Where(t => UnUsedAccount.Contains(t.Iban)).ToList();
        Context.TopYarTmps.RemoveRange(results);
        Context.SaveChanges();
    }
}