using PlateDelivery.Core.Models.ServiceCodings;

namespace PlateDelivery.Core.Services.ServiceCodings;
public interface IServiceCodingService
{
    long CreateServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool EditServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool DeleteServiceCoding(long Id);
    bool IsServiceCodingExist(string ServiceCode, string CodeLevel4);

    ServiceCodingsViewModel GetServiceCodings(int pageId = 1, int take = 50, string filterByServiceName = "", string filterByServiceCode = "");
    CreateAndEditServiceCodeingViewModel GetById(long Id);
    List<CreateAndEditServiceCodeingViewModel> GetByServiceCode(string ServiceCode);
}
