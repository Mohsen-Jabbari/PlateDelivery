using OfficeOpenXml;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;

namespace PlateDelivery.Core.Services.TopYarTmps;

internal class TopYarTmpService : ITopYarTmpService
{
    private readonly ITopYarTmpRepository _repository;

    public TopYarTmpService(ITopYarTmpRepository repository)
    {
        _repository = repository;
    }

    public long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model)
    {
        if (!_repository.Exists(u => u.RetrivalRef == model.RetrivalRef))
        {
            var topYarTmp = new TopYarTmp(model.RetrivalRef, model.TrackingNo, model.TransactionDate, model.TransactionTime,
                model.FinancialDate, model.Iban, model.Amount, model.PrincipalAmount, model.CardNo, model.Terminal,
                model.InstallationPlace, model.ServiceCode, model.ServiceName, model.ProvinceName, model.SubProvince, model.ProvinceCode);
            _repository.Add(topYarTmp);
            _repository.SaveSync();
            return topYarTmp.Id;
        }
        return -1;
    }

    public void CreateTopYarTmpList(List<CreateAndEditTopYarTmpViewModel> models)
    {
        foreach (var model in models)
        {
            var topYarTmp = new TopYarTmp(model.RetrivalRef, model.TrackingNo, model.TransactionDate, model.TransactionTime,
                model.FinancialDate, model.Iban, model.Amount, model.PrincipalAmount, model.CardNo, model.Terminal,
                model.InstallationPlace, model.ServiceCode, model.ServiceName, model.ProvinceName, model.SubProvince, model.ProvinceCode);
            _repository.Add(topYarTmp);
        }
        _repository.SaveSync();
    }

    public bool DeleteTopYarTmp(long Id)
    {
        return _repository.DeleteTopYarTmp(Id);
    }

    public bool DeleteTopYarTmp(List<long> Ids)
    {
        return _repository.DeleteTopYarTmp(Ids);
    }

    public void DeleteTopYarTmp()
    {
        _repository.DeleteAllTopYarTmp();
    }

    public void DeleteTopYarTmpRecord(long Id)
    {
        _repository.DeleteTopYarTmp(Id);
    }

    public void ExceptTopYarTmpRecord(long Id)
    {
        _repository.ExceptTopYarTmp(Id);
    }

    public void DeleteUnUsedRecords(List<string> UnUsedAccounts)
    {
        _repository.DeleteUnUsedTopYarTmp(UnUsedAccounts);
    }

    public bool EditTopyarTmp(CreateAndEditTopYarTmpViewModel model)
    {
        var oldTopYarTmp = _repository.GetTrackingSync(model.Id);
        if (oldTopYarTmp != null)
        {
            oldTopYarTmp.Edit(model.RetrivalRef, model.TrackingNo, model.TransactionDate, model.TransactionTime,
                model.FinancialDate, model.Iban, model.Amount, model.PrincipalAmount, model.CardNo, model.Terminal,
                model.InstallationPlace, model.ServiceCode, model.ServiceName, model.ProvinceName, model.SubProvince, model.ProvinceCode);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public CreateAndEditTopYarTmpViewModel GetById(long Id)
    {
        var result = _repository.GetTrackingSync(Id);
        if (result != null)
            return new CreateAndEditTopYarTmpViewModel()
            {
                Id = result.Id,
                TrackingNo = result.TrackingNo,
                TransactionDate = result.TransactionDate,
                TransactionTime = result.TransactionTime,
                RetrivalRef = result.RetrivalRef,
                Amount = result.Amount,
                PrincipalAmount = result.PrincipalAmount,
                CardNo = result.CardNo,
                Terminal = result.Terminal,
                InstallationPlace = result.InstallationPlace,
                ServiceCode = result.ServiceCode,
                ServiceName = result.ServiceName,
                ProvinceName = result.ProvinceName,
                CreationDate = result.CreationDate,
                FinancialDate = result.FinancialDate,
                Iban = result.Iban,
                SubProvince = result.SubProvince,
                ProvinceCode = result.ProvinceCode,
            };
        return null;
    }

    //public TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50, string? filterByRRN = "", string? filterByTrackingNo = "",
    //    string? filterByTransactionDate = "", string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "",
    //        string? filterByServiceCode = "", string? filterByProvinceName = "", string filterBySubProvince = "")
    //{
    //    var result = _repository.GetAll(); //lazyLoad;

    //    if (result != null)
    //    {
    //        var nullProvince = result.Where(r => r.ProvinceName == null || r.ProvinceName == string.Empty ||
    //        r.SubProvince == null || r.SubProvince == string.Empty).ToList();
    //        if (nullProvince.Count == 0)
    //        {
    //            if (!string.IsNullOrEmpty(filterByRRN))
    //            {
    //                result = result.Where(u => u.RetrivalRef.Contains(filterByRRN)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByTrackingNo))
    //            {
    //                result = result.Where(u => u.TrackingNo.Contains(filterByTrackingNo)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByTransactionDate))
    //            {
    //                result = result.Where(u => u.TransactionDate.Contains(filterByTransactionDate)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByIban))
    //            {
    //                result = result.Where(u => u.Iban.Contains(filterByRRN)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByAmount))
    //            {
    //                result = result.Where(u => u.Amount.Contains(filterByAmount) || u.PrincipalAmount.Contains(filterByAmount)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByTerminal))
    //            {
    //                result = result.Where(u => u.Terminal.Contains(filterByTerminal)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByServiceCode))
    //            {
    //                result = result.Where(u => u.ServiceCode.Contains(filterByServiceCode)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterByProvinceName))
    //            {
    //                result = result.Where(u => u.ProvinceName.Contains(filterByProvinceName)).ToList();
    //            }

    //            if (!string.IsNullOrEmpty(filterBySubProvince))
    //            {
    //                result = result.Where(u => u.SubProvince.Contains(filterBySubProvince)).ToList();
    //            }

    //            int takeData = take;
    //            int skip = (pageId - 1) * takeData;

    //            TopYarTmpViewModel list = new TopYarTmpViewModel();
    //            list.TopYarTmps = result.OrderByDescending(u => u.RetrivalRef).Skip(skip).Take(takeData).ToList();
    //            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
    //            list.CurrentPage = pageId;
    //            list.TopYarTmpCounts = result.Count;
    //            return list;
    //        }
    //        else
    //        {

    //            int takeData = take;
    //            int skip = (pageId - 1) * takeData;

    //            TopYarTmpViewModel list = new TopYarTmpViewModel();
    //            list.TopYarTmps = nullProvince.OrderByDescending(u => u.RetrivalRef).Skip(skip).Take(takeData).ToList();
    //            list.PageCount = (int)Math.Ceiling(nullProvince.Count / (double)takeData);
    //            list.CurrentPage = pageId;
    //            list.TopYarTmpCounts = nullProvince.Count;
    //            list.ProvinceMessage = "در داده های ورودی تعداد " + nullProvince.Count.ToString() + " رکورد بدون استان و شهر وجود دارد. لطفا اصلاح نمایید. ";
    //            return list;
    //        }
    //    }
    //    return new TopYarTmpViewModel();
    //}

    public TopYarTmpViewModel GetTopYarNullProvince(int pageId = 1, int take = 50)
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            var nullProvince = result.Where(r => r.ProvinceName == null || r.ProvinceName == string.Empty ||
            r.SubProvince == null || r.SubProvince == string.Empty).ToList();
            if (nullProvince.Count != 0)
            {
                int takeData = take;
                int skip = (pageId - 1) * takeData;

                TopYarTmpViewModel list = new TopYarTmpViewModel();
                list.TopYarTmps = nullProvince.OrderByDescending(u => u.RetrivalRef).Skip(skip).Take(takeData).ToList();
                list.PageCount = (int)Math.Ceiling(nullProvince.Count / (double)takeData);
                list.CurrentPage = pageId;
                list.TopYarTmpCounts = nullProvince.Count;
                list.ProvinceMessage = "در داده های ورودی تعداد " + nullProvince.Count.ToString() + " رکورد بدون استان و شهر وجود دارد. لطفا اصلاح نمایید. ";
                return list;
            }
        }
        return new TopYarTmpViewModel();
    }

    public TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50, string? filterByRRN = "", string? filterByTrackingNo = "",
        string? filterByTransactionDate = "", string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "",
            string? filterByServiceCode = "", string? filterByProvinceName = "", string filterBySubProvince = "")
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
                if (!string.IsNullOrEmpty(filterByRRN))
                {
                    result = result.Where(u => u.RetrivalRef.Contains(filterByRRN)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByTrackingNo))
                {
                    result = result.Where(u => u.TrackingNo.Contains(filterByTrackingNo)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByTransactionDate))
                {
                    result = result.Where(u => u.TransactionDate.Contains(filterByTransactionDate)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByIban))
                {
                    result = result.Where(u => u.Iban.Contains(filterByRRN)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByAmount))
                {
                    result = result.Where(u => u.Amount.Contains(filterByAmount) || u.PrincipalAmount.Contains(filterByAmount)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByTerminal))
                {
                    result = result.Where(u => u.Terminal.Contains(filterByTerminal)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByServiceCode))
                {
                    result = result.Where(u => u.ServiceCode.Contains(filterByServiceCode)).ToList();
                }

                if (!string.IsNullOrEmpty(filterByProvinceName))
                {
                    result = result.Where(u => u.ProvinceName.Contains(filterByProvinceName)).ToList();
                }

                if (!string.IsNullOrEmpty(filterBySubProvince))
                {
                    result = result.Where(u => u.SubProvince.Contains(filterBySubProvince)).ToList();
                }

                int takeData = take;
                int skip = (pageId - 1) * takeData;

                TopYarTmpViewModel list = new TopYarTmpViewModel();
                list.TopYarTmps = result.OrderByDescending(u => u.RetrivalRef).Skip(skip).Take(takeData).ToList();
                list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
                list.CurrentPage = pageId;
                list.TopYarTmpCounts = result.Count;
                return list;
        }
        return new TopYarTmpViewModel();
    }

    public TopYarTmpViewModel GetTopYarTmps()
    {
        var result = _repository.GetAll(); //lazyLoad;
        TopYarTmpViewModel list = new TopYarTmpViewModel();
        list.TopYarTmps = result.OrderByDescending(u => u.RetrivalRef).ToList();
        list.TopYarTmpCounts = result.Count;
        return list;
    }

    public TopYarTmpViewModel GetTopYarTmpsForDocument()
    {
        var result = _repository.GetAll(); //lazyLoad;
        TopYarTmpViewModel list = new()
        {
            TopYarTmps = result.OrderByDescending(u => u.ProvinceCode)
                                        .ThenBy(u => u.Terminal)
                                                .ThenBy(u => u.ServiceCode).ToList(),
            TopYarTmpCounts = result.Count
        };
        return list;
    }

    public TopYarTmpViewModel GetTopYarTmps(int pageId = 1, int take = 50)
    {
        var result = _repository.GetAll(); //lazyLoad;
        int takeData = take;
        int skip = (pageId - 1) * takeData;

        TopYarTmpViewModel list = new TopYarTmpViewModel();
        list.TopYarTmps = result.OrderByDescending(u => u.RetrivalRef).Skip(skip).Take(takeData).ToList();
        list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
        list.CurrentPage = pageId;
        list.TopYarTmpCounts = result.Count;
        return list;
    }

    public TopYarTmpViewModel GetTopYarTmps(List<string> rrn)
    {
        var result = _repository.GetAll(); //lazyLoad;
        TopYarTmpViewModel list = new TopYarTmpViewModel();
        list.TopYarTmps = result.OrderByDescending(u => u.RetrivalRef)
                    .Where(u => rrn.Contains(u.RetrivalRef)).ToList();
        list.TopYarTmpCounts = result.Count;
        list.TopYarTmps.Take(100).ToList();
        return list;
    }

    public bool IsTopYarTmpExist(string retrivalRefNo)
    {
        return _repository.Exists(u => u.RetrivalRef == retrivalRefNo);
    }

    public string GetFirstTopYarRecord()
    {
        var result = _repository.GetTopYarTmpFirstRecord();
        if (result != null)
            return result.TransactionDate.Trim();
        return null;
    }

    public List<CreateAndEditTopYarTmpViewModel> ReadDataFromExcel(string filePath)
    {
        var topYar = new List<CreateAndEditTopYarTmpViewModel>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var tmp = new CreateAndEditTopYarTmpViewModel
                {
                    RetrivalRef = worksheet.Cells[row, 1].Value.ToString(),
                    TrackingNo = worksheet.Cells[row, 2].Value.ToString(),
                    TransactionDate = worksheet.Cells[row, 3].Value.ToString(),
                    FinancialDate = worksheet.Cells[row, 4].Value.ToString(),
                    Iban = worksheet.Cells[row, 5].Value.ToString(),
                    Amount = worksheet.Cells[row, 6].Value.ToString(),
                    PrincipalAmount = worksheet.Cells[row, 7].Value.ToString(),
                    CardNo = worksheet.Cells[row, 8].Value.ToString(),
                    Terminal = worksheet.Cells[row, 9].Value.ToString(),
                    InstallationPlace = worksheet.Cells[row, 10].Value.ToString(),
                    ServiceCode = worksheet.Cells[row, 11].Value.ToString(),
                    ServiceName = worksheet.Cells[row, 12].Value.ToString(),
                    ProvinceName = (string)(worksheet.Cells[row, 13].Value ?? null),
                    SubProvince = (string)(worksheet.Cells[row, 14].Value ?? null),
                    TransactionTime = worksheet.Cells[row, 15].Value.ToString(),
                    ProvinceCode = "",
                    CreationDate = DateTime.Now,
                    // Map other properties accordingly
                };

                topYar.Add(tmp);
            }
        }

        return topYar;
    }
}