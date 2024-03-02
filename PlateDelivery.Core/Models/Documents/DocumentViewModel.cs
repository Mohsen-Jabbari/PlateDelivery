using PlateDelivery.DataLayer.Entities.DocumentAgg;

namespace PlateDelivery.Core.Models.Documents;

public class DocumentViewModel
{
    public List<Document> Documents { get; set; }
    public int DocumentCounts { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}