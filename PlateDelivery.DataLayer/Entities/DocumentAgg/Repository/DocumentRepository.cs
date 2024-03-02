using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;

internal class DocumentRepository : BaseRepository<Document>, IDocumentRepository
{
    public DocumentRepository(PlateDeliveryDBContext context) : base(context)
    {
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
}