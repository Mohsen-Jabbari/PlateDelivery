using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Models.Provinces;

namespace PlateDelivery.Core.Services.Accounts;
public interface IAccountService
{
    long CreateAccount(CreateAndEditAccountViewModel model);
    bool EditAccount(CreateAndEditAccountViewModel model);
    bool DeleteAccount(long Id);
    bool IsAccountExist(string BankCode);

    AccountsViewModel GetAccounts(int pageId = 1, int take = 50, string filterByBankCode = "");
    CreateAndEditAccountViewModel GetById(long Id);
}
