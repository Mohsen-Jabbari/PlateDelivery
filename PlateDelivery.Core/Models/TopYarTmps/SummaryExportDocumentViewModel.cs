namespace PlateDelivery.Core.Models.TopYarTmps;

public class SummaryExportDocumentViewModel
{
    public List<SummaryDocuments> SummaryDocuments { get; set; }
    public int SummaryCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool UnableToSend { get; set; }
}
