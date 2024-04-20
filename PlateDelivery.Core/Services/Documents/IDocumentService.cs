using PlateDelivery.Core.Models.Documents;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.DataLayer.Entities.DocumentAgg;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;

namespace PlateDelivery.Core.Services.Documents;
public interface IDocumentService
{
    long CreateDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder);
    long CreateDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder);
    long CreateIncomeDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder);
    long CreateTaxDocument(TopYarTmp topYar, ServiceCoding service, long maxOrder);
    long CreateTaxDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder);
    long CreateTaxTaxIncomeDocument(TopYarTmp topYar, List<ServiceCoding> services, long maxOrder);
    bool EditDocument(CreateAndEditDocumentViewModel model);
    bool DeleteDocument(long Id);
    void SaveChanges();

    SummaryExportDocumentViewModel GetMainHeadListOfDocumentsForExport(int pageId = 1, int take = 10, DocumentYears Year = DocumentYears.NotSelected, DocumentMonth Month = DocumentMonth.NotSelected);
    DocumentViewModel GetDocumentsByDocDate(string docDate, int pageId = 1, int take = 10);
    List<Document> GetDocumentsByDocDate(string docDate);
    List<Document> GetDocumentsByDocDateForTax(string docDate);
    long GetMaxOrder();
    bool IsDocumentDateExists(string docDate);
}
