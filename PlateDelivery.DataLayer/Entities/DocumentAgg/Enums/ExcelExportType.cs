using System.ComponentModel;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

public enum ExcelExportType
{
    [Description("خروجی جهت سامانه سیاق")]
    SiaghType = 1,
    [Description("خروجی جهت اداره مالیات")]
    TaxType = 2,
    [Description("خروجی کلی")]
    GeneralType = 3,
}
