﻿using PlateDelivery.Core.Models.TopYarTmps;

namespace PlateDelivery.Core.Services.TopYarTmps;
public interface ITopYarTmpService
{
    long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model);
    void CreateTopYarTmpList(List<CreateAndEditTopYarTmpViewModel> models);
    bool EditTopyarTmp(CreateAndEditTopYarTmpViewModel model);
    bool DeleteTopYarTmp(long Id);
    bool DeleteTopYarTmp(List<long> Ids);
    bool IsTopYarTmpExist(string retrivalRefNo);
    void DeleteTopYarTmp();
    void DeleteTopYarTmpRecord(long Id);
    void ExceptTopYarTmpRecord(long Id);
    void DeleteUnUsedRecords(List<string> UnUsedAccounts);

    TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50,
                  string? filterByRRN = "", string? filterByTrackingNo = "", string? filterByTransactionDate = "",
                    string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "", string? filterByServiceCode = "",
                       string? filterByProvinceName = "", string? filterBySubProvince = "");
    TopYarTmpViewModel GetTopYarNullProvince(int pageId = 1, int take = 50);
    TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50);
    TopYarTmpViewModel GetTopYarTmps();
    TopYarTmpViewModel GetTopYarTmpsForDocument();
    TopYarTmpViewModel GetTopYarTmps(List<string> rrn);
    CreateAndEditTopYarTmpViewModel GetById(long Id);
    string GetFirstTopYarRecord();
    List<CreateAndEditTopYarTmpViewModel> ReadDataFromExcel(string filePath);
}
