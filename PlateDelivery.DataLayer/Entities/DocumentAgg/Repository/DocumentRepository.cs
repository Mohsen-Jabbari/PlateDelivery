using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Types;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;

internal class DocumentRepository : BaseRepository<Document>, IDocumentRepository
{
    public DocumentRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public IQueryable<Document> GetDocumentByDate(string docDate)
    {
        return Context.Documents.Where(d => d.TransactionDate == docDate);
    }

    public long GetMaxOrder()
    {
        var maxOrder = Context.Documents.Select(d => d.Order).ToList();
        long maxOrderForReturn = maxOrder.DefaultIfEmpty(0).Max();
        if (maxOrderForReturn == 0)
        {
            return maxOrderForReturn;
        }
        else
            return ++maxOrderForReturn;
    }

    public DocumentMonth GetMonth(string thisDate)
    {
        var month = thisDate.Trim().Substring(4, 2);
        DocumentMonth st;
        _ = month switch
        {
            "01" => Enum.TryParse("Farvardin", out st),
            "02" => Enum.TryParse("Ordibehesht", out st),
            "03" => Enum.TryParse("Khordad", out st),
            "04" => Enum.TryParse("Tir", out st),
            "05" => Enum.TryParse("Mordad", out st),
            "06" => Enum.TryParse("Shahrivar", out st),
            "07" => Enum.TryParse("Mehr", out st),
            "08" => Enum.TryParse("Aban", out st),
            "09" => Enum.TryParse("Azar", out st),
            "10" => Enum.TryParse("Dey", out st),
            "11" => Enum.TryParse("Bahman", out st),
            "12" => Enum.TryParse("Esfand", out st),
            _ => Enum.TryParse("NotSelected", out st),
        };
        return st;
    }

    public IQueryable<ExportSummaryType> GetSummary()
    {
        return Context.Documents
            .Select(s => new ExportSummaryType()
            {
                DocumentYear = s.Year,
                DocumentMonth = s.Month,
                RetrivalRef = s.RetrivalRef,
                TransactionDate = s.TransactionDate,
            }).AsQueryable();
    }

    public DocumentYears GetYear(string thisDate)
    {
        var year = "Year" + thisDate.Trim()[..4];
        Enum.TryParse(year, out DocumentYears st);
        return st;
    }
}