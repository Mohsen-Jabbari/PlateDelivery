using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Types;
public class ExportSummaryType
{
    public DocumentYears DocumentYear { get; set; }
    public DocumentMonth DocumentMonth { get; set; }
    public string TransactionDate { get; set; }
    public string RetrivalRef { get; set; }
}
