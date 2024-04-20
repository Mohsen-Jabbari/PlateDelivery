using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;
using System.Linq;

namespace PlateDelivery.Core.Services.ServiceCodings;

internal class ServiceCodingService : IServiceCodingService
{
    private readonly IServiceCodingRepository _repository;

    public ServiceCodingService(IServiceCodingRepository repository)
    {
        _repository = repository;
    }

    public long CreateServiceCoding(CreateAndEditServiceCodeingViewModel model)
    {
        if (!_repository.Exists(u => u.ServiceCode == model.ServiceCode && u.CodeLevel4 == model.CodeLevel4 && u.CertainId == model.CertainId))
        {
            var serviceCoding = new ServiceCoding(model.ServiceName, model.ServiceFullName, model.ServiceCode,
                model.CodeLevel4, model.CodeLevel6, model.CertainId, model.Amount.ToString(), model.IncludeTax);
            _repository.Add(serviceCoding);
            _repository.SaveSync();
            return serviceCoding.Id;
        }
        return -1;
    }

    public bool DeleteServiceCoding(long Id)
    {
        var serviceCoding = _repository.GetTrackingSync(Id);
        if (serviceCoding != null)
        {
            _repository.DeleteServiceCoding(Id);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EditServiceCoding(CreateAndEditServiceCodeingViewModel model)
    {
        var oldServiceCoding = _repository.GetTrackingSync(model.Id);
        if (oldServiceCoding != null)
        {
            oldServiceCoding.Edit(model.ServiceName, model.ServiceFullName, model.ServiceCode,
                model.CodeLevel4, model.CodeLevel6, model.CertainId, model.Amount.ToString(), model.IncludeTax);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public CreateAndEditServiceCodeingViewModel GetById(long Id)
    {
        var result = _repository.GetTrackingSync(Id);
        if (result != null)
            return new CreateAndEditServiceCodeingViewModel()
            {
                Id = result.Id,
                ServiceCode = result.ServiceCode,
                ServiceFullName = result.ServiceFullName,
                CodeLevel4 = result.CodeLevel4,
                CodeLevel6 = result.CodeLevel6,
                ServiceName = result.ServiceName,
                CertainId = result.CertainId,
                CreationDate = result.CreationDate,
                Amount = long.Parse(result.Amount),
                IncludeTax = result.IncludeTax
            };
        return null;
    }

    public List<CreateAndEditServiceCodeingViewModel> GetByServiceCode(string ServiceCode)
    {
        var lazyResult = _repository.GetAll();
        var result = lazyResult.Where(r => r.ServiceCode == ServiceCode).ToList();
        List<CreateAndEditServiceCodeingViewModel> poorResult = new();
        if (result != null)
        {
            foreach (var item in result)
            {
                poorResult.Add(new CreateAndEditServiceCodeingViewModel()
                {
                    Id = item.Id,
                    ServiceCode = item.ServiceCode,
                    ServiceFullName = item.ServiceFullName,
                    CodeLevel4 = item.CodeLevel4,
                    CodeLevel6 = item.CodeLevel6,
                    ServiceName = item.ServiceName,
                    CertainId = item.CertainId,
                    CreationDate = item.CreationDate,
                    Amount = long.Parse(item.Amount),
                    IncludeTax = item.IncludeTax
                });
            }
            return poorResult;
        }
        return null;
    }

    public ServiceCodingsViewModel GetServiceCodings()
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            ServiceCodingsViewModel list = new ServiceCodingsViewModel();
            list.ServiceCodings = result.OrderByDescending(u => u.ServiceCode).ToList();
            list.ServiceCodingsCounts = result.Count;
            return list;
        }
        return new ServiceCodingsViewModel();
    }

    public ServiceCodingsViewModel GetServiceCodings(int pageId = 1, int take = 50, string filterByServiceName = "", string filterByServiceCode = "")
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByServiceName))
            {
                result = result.Where(u => u.ServiceName.Contains(filterByServiceName) || u.ServiceFullName.Contains(filterByServiceName)).ToList();
            }

            if (!string.IsNullOrEmpty(filterByServiceCode))
            {
                result = result.Where(u => u.ServiceCode.Contains(filterByServiceCode)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            ServiceCodingsViewModel list = new ServiceCodingsViewModel();
            list.ServiceCodings = result.OrderByDescending(u => u.ServiceCode).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.ServiceCodingsCounts = result.Count;
            return list;
        }
        return new ServiceCodingsViewModel();
    }

    public bool IsServiceCodingExist(string ServiceCode, string CodeLevel4, long CertainId)
    {
        return _repository.Exists(u => u.ServiceCode == ServiceCode && u.CodeLevel4 == CodeLevel4 && u.CertainId == CertainId);
    }
}