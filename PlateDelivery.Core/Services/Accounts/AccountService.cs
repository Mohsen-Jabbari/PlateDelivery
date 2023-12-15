using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.DataLayer.Entities.AccountAgg;
using PlateDelivery.DataLayer.Entities.AccountAgg.Repository;

namespace PlateDelivery.Core.Services.Accounts;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public long CreateAccount(CreateAndEditAccountViewModel model)
    {
        if (!_repository.Exists(u => u.Iban == model.Iban))
        {
            var account = new Account(model.Iban, model.BankCode, model.BankName);
            _repository.Add(account);
            _repository.SaveSync();
            return account.Id;
        }
        return -1;
    }

    public bool DeleteAccount(long Id)
    {
        var account = _repository.GetTrackingSync(Id);
        if (account != null)
        {
            _repository.DeleteAccount(Id);
            _repository.SaveSync();
            return true;
        }
        return false;
    }

    public bool EditAccount(CreateAndEditAccountViewModel model)
    {
        var oldAccount = _repository.GetTrackingSync(model.Id);
        if (oldAccount != null)
        {
            oldAccount.Edit(model.Iban, model.BankCode, model.BankName);
            _repository.SaveSync();
            return true;
        }
        return false;
    }
}
