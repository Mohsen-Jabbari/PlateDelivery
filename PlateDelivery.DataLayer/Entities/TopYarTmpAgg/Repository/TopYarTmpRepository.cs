using Microsoft.EntityFrameworkCore;
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

        Context.TopYarTmps.ExecuteDelete();
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

    public bool ExceptTopYarTmp(long Id)
    {
        var item = Context.TopYarTmps.FirstOrDefault(x => x.Id == Id);
        if (item != null)
        {
            item.ExceptTopYarRecord();
            Context.TopYarTmps.Update(item);
            Context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteTopYarTmp(List<long> Ids)
    {
        // حذف مستقیم رکوردها از دیتابیس بدون واکشی آن‌ها
        //var deletedCount = Context.TopYarTmps
        //.Where(x => Ids.Contains(x.Id))
        //.ExecuteDelete();

        var idList = string.Join(",", Ids);
        var sql = $"DELETE FROM TopYarTmps WHERE Id IN ({idList})";
        var deletedCount = Context.Database.ExecuteSqlRaw(sql);

        // اگر حداقل یک رکورد حذف شده باشد، true برگردان
        if (deletedCount > 0)
            return true;
        return false;

        //var items = Context.TopYarTmps.Where(x => Ids.Contains(x.Id)).ToList();
        //if (items != null)
        //{
        //    Context.TopYarTmps.RemoveRange(items);
        //    Context.SaveChanges();
        //    return true;
        //}
        //return false;
    }

    public void DeleteUnUsedTopYarTmp(List<string> UnUsedAccount)
    {
        var deletedCount = Context.TopYarTmps
        .Where(x => UnUsedAccount.Contains(x.Iban))
        .ExecuteDelete();
    }

    public TopYarTmp GetTopYarTmpFirstRecord()
    {
        var result = Context.TopYarTmps.FirstOrDefault();
        if (result != null)
            return result;
        return null;
    }
}