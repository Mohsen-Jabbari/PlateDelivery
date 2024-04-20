using PlateDelivery.Core.Models.ServiceCodings;
using System.Net.Sockets;

namespace PlateDelivery.Core.Services.ServiceCodings;
public interface IServiceCodingService
{
    long CreateServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool EditServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool DeleteServiceCoding(long Id);
    bool IsServiceCodingExist(string ServiceCode, string CodeLevel4, long CertainId);

    ServiceCodingsViewModel GetServiceCodings(int pageId = 1, int take = 50, string filterByServiceName = "", string filterByServiceCode = "");
    ServiceCodingsViewModel GetServiceCodings();
    CreateAndEditServiceCodeingViewModel GetById(long Id);
    List<CreateAndEditServiceCodeingViewModel> GetByServiceCode(string ServiceCode);
}
