using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.DataLayer.Entities.ProvinceAgg;
using PlateDelivery.DataLayer.Entities.ProvinceAgg.Repository;

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

    public EditProvinceViewModel GetById(long Id)
    {
        var result = _repository.GetTrackingSync(Id);
        if (result != null)
            return new EditProvinceViewModel()
            {
                Id = result.Id,
                ProvinceName = result.ProvinceName,
                SubProvince = result.SubProvince,
                ProvinceCode = result.ProvinceCode
            };
        return null;
    }

    public ProvincesViewModel GetProvinces(int pageId = 1, int take = 50, string filterByProvince = "")
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByProvince))
            {
                result = result.Where(u => u.ProvinceName.Contains(filterByProvince)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            ProvincesViewModel list = new ProvincesViewModel();
            list.Provinces = result.OrderBy(u => u.ProvinceName).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.ProvincesCounts = result.Count;
            return list;
        }
        return new ProvincesViewModel();
    }

    public bool IsProvinceExist(string ProvinceCode)
    {
        return _repository.Exists(u => u.ProvinceCode == ProvinceCode);
    }
}