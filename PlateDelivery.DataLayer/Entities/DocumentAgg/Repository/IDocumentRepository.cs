using PlateDelivery.Common.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Types;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;
public interface IDocumentRepository : IBaseRepository<Document>
{
    long GetMaxOrder();
    DocumentYears GetYear(string thisDate);
    DocumentMonth GetMonth(string thisDate);
    IQueryable<ExportSummaryType> GetSummary();
    IQueryable<Document> GetDocumentByDate(string docDate);
}
