using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.UserAgg.Repository;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PlateDeliveryDBContext context) : base(context)
    {

    }
}