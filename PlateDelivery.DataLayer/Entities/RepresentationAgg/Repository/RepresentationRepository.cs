using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.RepresentationAgg.Repository;

internal class RepresentationRepository : BaseRepository<Representation>, IRepresentationRepository
{
    public RepresentationRepository(PlateDeliveryDBContext context) : base(context)
    {

    }
}
