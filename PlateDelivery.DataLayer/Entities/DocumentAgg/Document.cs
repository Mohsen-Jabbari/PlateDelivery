using PlateDelivery.Common.CommonClasses;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg;
public class Document : BaseEntity
{
    public Document(long order, string retrivalRef, string trackingNo, string transactionDate,
        string transactionTime, string financialDate, string iban, string amount,
        string principalAmount, string cardNo, string terminal, string installationPlace,
        string serviceCode, string serviceName, string? provinceName, string? subProvince,
        string? provinceCode, string? certainCode, string? codeLevel4, string? codeLevel5,
        string? codeLevel6, string? description, string? debt, string? credit, DocumentYears year, DocumentMonth month)
    {
        Order = order;
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
        CertainCode = certainCode;
        CodeLevel4 = codeLevel4;
        CodeLevel5 = codeLevel5;
        CodeLevel6 = codeLevel6;
        Description = description;
        Debt = debt;
        Credit = credit;
        Year = year;
        Month = month;
    }

    public void Edit(long order, string retrivalRef, string trackingNo, string transactionDate,
        string transactionTime, string financialDate, string iban, string amount,
        string principalAmount, string cardNo, string terminal, string installationPlace,
        string serviceCode, string serviceName, string? provinceName, string? subProvince,
        string? provinceCode, string? certainCode, string? codeLevel4, string? codeLevel5,
        string? codeLevel6, string? description, string? debt, string? credit, DocumentYears year, DocumentMonth month)
    {
        Order = order;
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
        CertainCode = certainCode;
        CodeLevel4 = codeLevel4;
        CodeLevel5 = codeLevel5;
        CodeLevel6 = codeLevel6;
        Description = description;
        Debt = debt;
        Credit = credit;
        Year = year;
        Month = month;
    }

    public DocumentYears Year { get; private set; }
    public DocumentMonth Month { get; private set; }
    public long Order { get; private set; }
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
    public string? CertainCode { get; private set; }
    public string? CodeLevel4 { get; private set; }
    public string? CodeLevel5 { get; private set; }
    public string? CodeLevel6 { get; private set; }
    public string? Description { get; private set; }
    public string? Debt { get; private set; }
    public string? Credit { get; private set; }
}
