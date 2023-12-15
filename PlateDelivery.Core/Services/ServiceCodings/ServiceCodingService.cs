using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg.Repository;

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
        if (!_repository.Exists(u => u.ServiceCode == model.ServiceCode))
        {
            var serviceCoding = new ServiceCoding(model.ServiceName, model.ServiceFullName, model.ServiceCode,
                model.CodeLevel4, model.CodeLevel6);
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
                model.CodeLevel4, model.CodeLevel6);
            _repository.SaveSync();
            return true;
        }
        return false;
    }
}