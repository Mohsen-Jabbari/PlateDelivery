using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg.Repository;

namespace PlateDelivery.Core.Services.TopYarTmps;

internal class TopYarTmpService : ITopYarTmpService
{
    private readonly ITopYarTmpRepository _repository;

    public long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model)
    {
        if (!_repository.Exists(u => u.RetrivalRef == model.RetrivalRef))
        {
            var topYarTmp = new TopYarTmp(model.RetrivalRef, model.TrackingNo, model.TransactionDate, model.TransactionTime,
                model.FinancialDate, model.Iban, model.Amount, model.PrincipalAmount, model.CardNo, model.Terminal,
                model.InstallationPlace, model.ServiceCode, model.ServiceName, model.ProvinceName, model.SubProvince,
                model.ProvinceCode, model.CertainCode, model.CodeLever4, model.CodeLever5, model.CodeLever6, model.Description,
                model.TaxAmount, model.IncomeAmount);
            _repository.Add(topYarTmp);
            _repository.SaveSync();
            return topYarTmp.Id;
        }
        return -1;
    }

    public bool DeleteTopYarTmp(long Id)
    {
        var topYarTmp = _repository.GetTrackingSync(Id);
        if (topYarTmp != null)
        {
            _repository.DeleteTopYarTmp(Id);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EcitTopyarTmp(CreateAndEditTopYarTmpViewModel model)
    {
        var oldTopYarTmp = _repository.GetTrackingSync(model.Id);
        if (oldTopYarTmp != null)
        {
            oldTopYarTmp.Edit(model.RetrivalRef, model.TrackingNo, model.TransactionDate, model.TransactionTime,
                model.FinancialDate, model.Iban, model.Amount, model.PrincipalAmount, model.CardNo, model.Terminal,
                model.InstallationPlace, model.ServiceCode, model.ServiceName, model.ProvinceName, model.SubProvince,
                model.ProvinceCode, model.CertainCode, model.CodeLever4, model.CodeLever5, model.CodeLever6, model.Description,
                model.TaxAmount, model.IncomeAmount);
            _repository.SaveSync();
            return true;
        }
        return false;
    }
}