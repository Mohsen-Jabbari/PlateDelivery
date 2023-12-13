using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.AccountAgg.Repository;

internal class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(PlateDeliveryDBContext context) : base(context)
    {
    }
}