using PlateDelivery.Core.Models.TopYarTmps;

namespace PlateDelivery.Core.Services.TopYarTmps;
public interface ITopYarTmpService
{
    long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model);
    bool EditTopyarTmp(CreateAndEditTopYarTmpViewModel model);
    bool DeleteTopYarTmp(long Id);
    bool IsTopYarTmpExist(string retrivalRefNo);

    TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50,
                  string? filterByRRN = "", string? filterByTrackingNo = "", string? filterByTransactionDate = "",
                    string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "", string? filterByServiceCode = "",
                       string? filterByProvinceName = "", string? filterBySubProvince = "");
    CreateAndEditTopYarTmpViewModel GetById(long Id);
}
