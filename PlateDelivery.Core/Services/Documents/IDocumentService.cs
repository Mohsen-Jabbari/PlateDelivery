using PlateDelivery.Core.Models.Documents;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;

namespace PlateDelivery.Core.Services.Documents;
public interface IDocumentService
{
    long CreateDocument(TopYarTmp topYar, ServiceCoding service);
    long CreateDocument(TopYarTmp topYar, List<ServiceCoding> services);
    long CreateIncomeDocument(TopYarTmp topYar, ServiceCoding service);
    long CreateTaxDocument(TopYarTmp topYar, ServiceCoding service);
    bool EditDocument(CreateAndEditDocumentViewModel model);
    bool DeleteDocument(long Id);
}
