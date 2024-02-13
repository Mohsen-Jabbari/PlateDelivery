using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Repository;

internal class DocumentRepository : BaseRepository<Document>, IDocumentRepository
{
    public DocumentRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}