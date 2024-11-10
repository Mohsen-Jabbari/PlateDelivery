using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

namespace PlateDelivery.Core.Models.TopYarTmps;

public class SummaryDocuments
{
    public string DocumentDate { get; set; }
    public DocumentYears Year { get; set; }
    public DocumentMonth Month { get; set; }
    public int Count { get; set; }
    public long CreditAmount { get; set; }
}