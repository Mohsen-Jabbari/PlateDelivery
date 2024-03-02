using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.AccountAgg.Repository;

internal class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public void DeleteAccount(long Id)
    {
        throw new NotImplementedException();
    }

    public Account? GetByIban(string Iban)
    {
        return Context.Accounts.Where(a => a.Iban.Trim() == Iban.Trim()).FirstOrDefault();
    }
}