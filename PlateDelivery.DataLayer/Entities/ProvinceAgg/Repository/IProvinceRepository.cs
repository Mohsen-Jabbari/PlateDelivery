using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;
public interface IProvinceRepository : IBaseRepository<Province>
{
    void DeleteProvince(long Id);
    Province? GetProvinceByNameAndSubName(string ProvinceName, string SubProvince);
}
