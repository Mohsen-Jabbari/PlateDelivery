using PlateDelivery.Core.Models.ServiceCodings;

namespace PlateDelivery.Core.Services.ServiceCodings;
public interface IServiceCodingService
{
    long CreateServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool EditServiceCoding(CreateAndEditServiceCodeingViewModel model);
    bool DeleteServiceCoding(long Id);
}
