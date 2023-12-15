using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;
using PlateDelivery.DataLayer.Entities.ProvinceAgg;

namespace PlateDelivery.Core.Services.Provinces;

internal class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;

    public ProvinceService(IProvinceRepository repository)
    {
        _repository = repository;
    }

    public long CreateProvince(CreateProvinceViewModel model)
    {
        if (!_repository.Exists(u => u.ProvinceCode == model.ProvinceCode))
        {
            var province = new Province(model.ProvinceName, model.SubProvince, model.ProvinceCode);
            _repository.Add(province);
            _repository.SaveSync();
            return province.Id;
        }
        return -1;
    }

    public bool DeleteProvince(long Id)
    {
        var province = _repository.GetTrackingSync(Id);
        if (province != null)
        {
            _repository.DeleteProvince(Id);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EditProvince(EditProvinceViewModel model)
    {
        var oldProvince = _repository.GetTrackingSync(model.Id);
        if (oldProvince != null)
        {
            oldProvince.Edit(model.ProvinceName, model.SubProvince, model.ProvinceCode);
            _repository.SaveSync();
            return true;
        }
        return false;
    }
}