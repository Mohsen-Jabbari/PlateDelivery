using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.AccountAgg;
public class Account : BaseEntity
{
    public Account(string iban, string bankCode, string bankName)
    {
        Guard(iban, bankCode, bankName);
        Iban = iban;
        BankCode = bankCode;
        BankName = bankName;
    }

    public void Edit(string iban, string bankCode, string bankName)
    {
        Guard(iban, bankCode, bankName);
        Iban = iban;
        BankCode = bankCode;
        BankName = bankName;
    }

    private Account()
    {

    }

    public static void Guard(string Iban, string BankCode, string BankName)
    {
        NullOrEmptyDataException.CheckString(Iban, nameof(Iban));
        NullOrEmptyDataException.CheckString(BankCode, nameof(BankCode));
        NullOrEmptyDataException.CheckString(BankName, nameof(BankName));
    }

    public string Iban { get; private set; }
    public string BankCode { get; private set; }
    public string BankName { get; private set; }
}