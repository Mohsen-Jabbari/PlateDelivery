using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.ProvinceAgg;
public class Province : BaseEntity
{
    public Province(string provinceName, string subProvince, string provinceCode, 
        string? codeLevel4)
    {
        Guard(provinceName, subProvince, provinceCode);
        ProvinceName = provinceName;
        SubProvince = subProvince;
        ProvinceCode = provinceCode;
        CodeLevel4 = codeLevel4;
    }

    public void Edit(string provinceName, string subProvince, string provinceCode,
        string? codeLevel4)
    {
        Guard(provinceName, subProvince, provinceCode);
        ProvinceName = provinceName;
        SubProvince = subProvince;
        ProvinceCode = provinceCode;
        CodeLevel4 = codeLevel4;
    }

    public static void Guard(string ProvinceName, string SubProvince, string ProvinceCode)
    {
        NullOrEmptyDataException.CheckString(ProvinceName, nameof(ProvinceName));
        NullOrEmptyDataException.CheckString(SubProvince, nameof(SubProvince));
        NullOrEmptyDataException.CheckString(ProvinceCode, nameof(ProvinceCode));
    }

    public string ProvinceName { get; private set; }
    public string SubProvince { get; private set; }
    public string ProvinceCode { get; private set; }
    public string? CodeLevel4 { get; private set; }
}
