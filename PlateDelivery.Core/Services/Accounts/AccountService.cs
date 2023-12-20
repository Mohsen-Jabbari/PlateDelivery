using PlateDelivery.Core.Models.Accounts;
using PlateDelivery.Core.Models.Provinces;
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

    public AccountsViewModel GetAccounts(int pageId = 1, int take = 50, string filterByBankCode = "")
    {
        var result = _repository.GetAll(); //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByBankCode))
            {
                result = result.Where(u => u.BankCode.Contains(filterByBankCode)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            AccountsViewModel list = new AccountsViewModel();
            list.Accounts = result.OrderByDescending(u => u.BankCode).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.AccountsCounts = result.Count;
            return list;
        }
        return new AccountsViewModel();
    }

    public CreateAndEditAccountViewModel GetById(long Id)
    {
        var result = _repository.GetTrackingSync(Id);
        if (result != null)
            return new CreateAndEditAccountViewModel()
            {
                Id = result.Id,
                Iban = result.Iban,
                BankCode = result.BankCode,
                BankName = result.BankName,
                CreationDate = result.CreationDate
            };
        return null;
    }

    public bool IsAccountExist(string BankCode)
    {
        return _repository.Exists(u => u.BankCode == BankCode);
    }
}
