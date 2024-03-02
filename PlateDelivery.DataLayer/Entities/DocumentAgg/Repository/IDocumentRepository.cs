using PlateDelivery.Common.Repository;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;
public interface IDocumentRepository : IBaseRepository<Document>
{
    long GetMaxOrder();
    DocumentYears GetYear(string thisDate);
    DocumentMonth GetMonth(string thisDate);
    IQueryable<Document> GetSummary();
}
