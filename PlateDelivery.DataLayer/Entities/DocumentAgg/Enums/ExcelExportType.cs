using System.ComponentModel;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

public enum ExcelExportType
{
    [Description("خروجی جهت سامانه سیاق")]
    SiaghType = 1,
    [Description("خروجی کلی جهت سامانه سیاق")]
    SiaghFullType = 2,
    [Description("خروجی جهت اداره مالیات")]
    TaxType = 3,
    [Description("خروجی کلی")]
    GeneralType = 4,
}
