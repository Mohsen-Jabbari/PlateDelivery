using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.AccountAgg.Repository;
public interface IAccountRepository : IBaseRepository<Account>
{
    void DeleteAccount(long Id);
}
