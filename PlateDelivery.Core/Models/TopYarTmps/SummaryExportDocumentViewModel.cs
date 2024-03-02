using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

namespace PlateDelivery.Core.Models.TopYarTmps;

public class SummaryExportDocumentViewModel
{
    public List<SummaryDocuments> SummaryDocuments { get; set; }
    public int SummaryCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool UnableToSend { get; set; }
}

public class SummaryDocuments
{
    public string DocumentDate { get; set; }
    public DocumentYears Year { get; set; }
    public DocumentMonth Month { get; set; }
    public int Count { get; set; }
}