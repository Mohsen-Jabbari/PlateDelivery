using PlateDelivery.Core.Models.Certains;
using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;
using PlateDelivery.DataLayer.Entities.CertainAgg.Repository;

namespace PlateDelivery.Core.Services.Certains;

internal class CertainService : ICertainService
{
    private readonly ICertainRepository _repository;

    public CertainService(ICertainRepository repository)
    {
        _repository = repository;
    }

    public long CreateCertain(CreateAndEditCertainViewModel model)
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

    public bool EditCertain(CreateAndEditCertainViewModel model)
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

    public CreateAndEditCertainViewModel GetById(long Id)
    {
        var result = _repository.GetTrackingSync(Id);
        if (result != null)
            return new CreateAndEditCertainViewModel()
            {
                Id = result.Id,
                CertainCode = result.CertainCode,
                Category = result.Category,
                CertainName = result.CertainName,
                CreationDate = result.CreationDate
            };
        return new CreateAndEditCertainViewModel();
    }

    public CertainsViewModel GetCertains(int pageId = 1, int take = 50, string filterByCertainCode = "")
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByCertainCode))
            {
                result = result.Where(u => u.CertainCode.Contains(filterByCertainCode)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            CertainsViewModel list = new CertainsViewModel();
            list.Certains = result.OrderByDescending(u => u.CertainCode).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.CertainsCounts = result.Count;
            return list;
        }
        return new CertainsViewModel();
    }

    public List<CertainDropDownListModel> GetIncomeCertain()
    {
        var Certains = _repository.GetAll();
        List<CertainDropDownListModel> result = new();
        foreach (var certain in Certains)
        {
            if (certain.Category == CertainCategory.Income || certain.Category == CertainCategory.Tax)
                result.Add(new CertainDropDownListModel()
                {
                    Id = certain.Id,
                    Text = certain.CertainName + " - " + certain.CertainCode
                });
        }
        return result;
    }

    public bool IsCertainExist(string CertainCode)
    {
        return _repository.Exists(u => u.CertainCode == CertainCode);
    }
}