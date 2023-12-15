using PlateDelivery.Core.Models.Certains;
using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg.Repository;

namespace PlateDelivery.Core.Services.Certains;

internal class CertainService : ICertainService
{
    private readonly ICertainRepository _repository;

    public CertainService(ICertainRepository repository)
    {
        _repository = repository;
    }

    public long CreateCertain(CreateCertainViewModel model)
    {
        if (!_repository.Exists(u => u.CertainCode == model.CertainCode))
        {
            var certain = new Certain(model.CertainName, model.CertainCode, model.Category);
            _repository.Add(certain);
            _repository.SaveSync();
            return certain.Id;
        }
        return -1;
    }

    public bool DeleteCertain(long Id)
    {
        var certain = _repository.GetTrackingSync(Id);
        if (certain != null)
        {
            _repository.DeleteCertain(Id);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EditCertain(CreateCertainViewModel model)
    {
        var oldCertain = _repository.GetTrackingSync(model.Id);
        if (oldCertain != null)
        {
            oldCertain.Edit(model.CertainName, model.CertainCode, model.Category);
            _repository.SaveSync();
            return true;
        }
        return false;
    }
}