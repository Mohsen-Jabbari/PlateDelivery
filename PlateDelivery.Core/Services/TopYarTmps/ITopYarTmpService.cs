using PlateDelivery.Core.Models.TopYarTmps;

namespace PlateDelivery.Core.Services.TopYarTmps;
public interface ITopYarTmpService
{
    long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model);
    bool EditTopyarTmp(CreateAndEditTopYarTmpViewModel model);
    bool DeleteTopYarTmp(long Id);
    bool DeleteTopYarTmp(List<long> Ids);
    bool IsTopYarTmpExist(string retrivalRefNo);
    void DeleteTopYarTmp();
    void DeleteUnUsedRecords(List<string> UnUsedAccounts);

    TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50,
                  string? filterByRRN = "", string? filterByTrackingNo = "", string? filterByTransactionDate = "",
                    string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "", string? filterByServiceCode = "",
                       string? filterByProvinceName = "", string? filterBySubProvince = "");
    TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50);
    TopYarTmpViewModel GetTopYarTmps();
    TopYarTmpViewModel GetTopYarTmpsForDocument();
    TopYarTmpViewModel GetTopYarTmps(List<string> rrn);
    CreateAndEditTopYarTmpViewModel GetById(long Id);
    string GetFirstTopYarRecord();
}
