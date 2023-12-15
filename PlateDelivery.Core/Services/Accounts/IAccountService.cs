using PlateDelivery.Core.Models.Accounts;

namespace PlateDelivery.Core.Services.Accounts;
public interface IAccountService
{
    long CreateAccount(CreateAndEditAccountViewModel model);
    bool EditAccount(CreateAndEditAccountViewModel model);
    bool DeleteAccount(long Id);
}
