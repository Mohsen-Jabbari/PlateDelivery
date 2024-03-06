using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.TopYarTmpAgg;
public class TopYarTmp : BaseEntity
{
    public TopYarTmp(string retrivalRef, string trackingNo, string transactionDate, string transactionTime,
        string financialDate, string iban, string amount, string principalAmount, string cardNo, string terminal,
        string installationPlace, string serviceCode, string serviceName, string provinceName, string subProvince,
        string provinceCode)
    {
        Guard(retrivalRef, transactionDate, iban, principalAmount, terminal, serviceCode);
        RetrivalRef = retrivalRef;
        TrackingNo = trackingNo;
        TransactionDate = transactionDate;
        TransactionTime = transactionTime;
        FinancialDate = financialDate;
        Iban = iban;
        Amount = amount;
        PrincipalAmount = principalAmount;
        CardNo = cardNo;
        Terminal = terminal;
        InstallationPlace = installationPlace;
        ServiceCode = serviceCode;
        ServiceName = serviceName;
        ProvinceName = provinceName;
        SubProvince = subProvince;
        ProvinceCode = provinceCode;
    }

    public void Edit(string retrivalRef, string trackingNo, string transactionDate, string transactionTime,
        string financialDate, string iban, string amount, string principalAmount, string cardNo, string terminal,
        string installationPlace, string serviceCode, string serviceName, string provinceName, string subProvince,
        string provinceCode)
    {
        Guard(retrivalRef, transactionDate, iban, principalAmount, terminal, serviceCode);
        RetrivalRef = retrivalRef;
        TrackingNo = trackingNo;
        TransactionDate = transactionDate;
        TransactionTime = transactionTime;
        FinancialDate = financialDate;
        Iban = iban;
        Amount = amount;
        PrincipalAmount = principalAmount;
        CardNo = cardNo;
        Terminal = terminal;
        InstallationPlace = installationPlace;
        ServiceCode = serviceCode;
        ServiceName = serviceName;
        ProvinceName = provinceName;
        SubProvince = subProvince;
        ProvinceCode = provinceCode;
    }

    public static void Guard(string RetrivalRef, string TransactionDate, string Iban, string PrincipalAmount, string Terminal,
        string ServiceCode)
    {
        NullOrEmptyDataException.CheckString(RetrivalRef, nameof(RetrivalRef));
        NullOrEmptyDataException.CheckString(TransactionDate, nameof(TransactionDate));
        NullOrEmptyDataException.CheckString(Iban, nameof(Iban));
        NullOrEmptyDataException.CheckString(PrincipalAmount, nameof(PrincipalAmount));
        NullOrEmptyDataException.CheckString(Terminal, nameof(Terminal));
        NullOrEmptyDataException.CheckString(ServiceCode, nameof(ServiceCode));
    }

    public string RetrivalRef { get; private set; }
    public string TrackingNo { get; private set; }
    public string TransactionDate { get; private set; }
    public string TransactionTime { get; private set; }
    public string FinancialDate { get; private set; }
    public string Iban { get; private set; }
    public string Amount { get; private set; }
    public string PrincipalAmount { get; private set; }
    public string CardNo { get; private set; }
    public string Terminal { get; private set; }
    public string InstallationPlace { get; private set; }
    public string ServiceCode { get; private set; }
    public string ServiceName { get; private set; }
    public string? ProvinceName { get; private set; }
    public string? SubProvince { get; private set; }
    public string? ProvinceCode { get; private set; }
}
