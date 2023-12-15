using System.ComponentModel.DataAnnotations;

namespace PlateDelivery.Core.Models.TopYarTmps;
public class CreateAndEditTopYarTmpViewModel : BaseDto
{
    [Required]
    public string RetrivalRef { get; set; }
    public string TrackingNo { get; set; }
    [Required]
    public string TransactionDate { get; set; }
    public string TransactionTime { get; set; }
    public string FinancialDate { get; set; }
    [Required]
    public string Iban { get; set; }
    public string Amount { get; set; }
    [Required]
    public string PrincipalAmount { get; set; }
    public string CardNo { get; set; }
    [Required]
    public string Terminal { get; set; }
    public string InstallationPlace { get; set; }
    [Required]
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    [Required]
    public string ProvinceName { get; set; }
    [Required]
    public string SubProvince { get; set; }
    public string ProvinceCode { get; set; }
    public string CertainCode { get; set; }
    public string CodeLever4 { get; set; }
    public string CodeLever5 { get; set; }
    public string CodeLever6 { get; set; }
    public string Description { get; set; }
    public string TaxAmount { get; set; }
    public string IncomeAmount { get; set; }
}
