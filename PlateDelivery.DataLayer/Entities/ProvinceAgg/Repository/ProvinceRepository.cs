using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;

internal class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public void DeleteProvince(long Id)
    {
        throw new NotImplementedException();
    }

    public Province? GetProvinceByNameAndSubName(string ProvinceName, string SubProvince)
    {
        var province = Context.Provinces
            .Where(p => p.ProvinceName == ProvinceName.Trim()).ToList();
        if (province != null)
        {
            if(province.Where(p => p.SubProvince == SubProvince.Trim()).Any())
            {
                return province.Where(p => p.SubProvince == SubProvince.Trim()).FirstOrDefault();
            }

            else
            {
                return province.Where(p => p.CodeLevel4 == null).FirstOrDefault();
            }
        }
        return null;
    }
}