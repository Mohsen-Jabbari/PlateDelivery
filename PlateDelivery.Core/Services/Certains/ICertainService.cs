using PlateDelivery.Core.Models.Certains;

namespace PlateDelivery.Core.Services.Certains;
public interface ICertainService
{
    long CreateCertain(CreateCertainViewModel model);
    bool EditCertain(CreateCertainViewModel model);
    bool DeleteCertain(long Id);
}
