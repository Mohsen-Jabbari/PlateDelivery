using PlateDelivery.Core.Models.TopYarTmps;

namespace PlateDelivery.Core.Services.TopYarTmps;
public interface ITopYarTmpService
{
    long CreateTopYarTmp(CreateAndEditTopYarTmpViewModel model);
    bool EcitTopyarTmp(CreateAndEditTopYarTmpViewModel model);
    bool DeleteTopYarTmp(long Id);
}
