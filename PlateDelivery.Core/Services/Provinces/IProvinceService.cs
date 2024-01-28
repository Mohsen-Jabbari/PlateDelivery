using PlateDelivery.Core.Models.Provinces;

namespace PlateDelivery.Core.Services.Provinces;
public interface IProvinceService
{
    long CreateProvince(CreateProvinceViewModel model);
    bool EditProvince(EditProvinceViewModel model);
    bool DeleteProvince(long Id);
    bool IsProvinceExist(string ProvinceCode,string CodeLevel4);

    ProvincesViewModel GetProvinces(int pageId = 1, int take = 50, string filterByProvince = "");
    EditProvinceViewModel GetById(long Id);
}
