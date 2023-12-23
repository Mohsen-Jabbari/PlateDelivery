using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Models.Certains;

namespace PlateDelivery.Core.Services.Certains;
public interface ICertainService
{
    long CreateCertain(CreateAndEditCertainViewModel model);
    bool EditCertain(CreateAndEditCertainViewModel model);
    bool DeleteCertain(long Id);
    bool IsCertainExist(string CertainCode);
    List<CertainDropDownListModel> GetIncomeCertain();

    CertainsViewModel GetCertains(int pageId = 1, int take = 50, string filterByCertainCode = "");
    CreateAndEditCertainViewModel GetById(long Id);
}
