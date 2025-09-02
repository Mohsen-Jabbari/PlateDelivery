using System.ComponentModel;

namespace PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

public enum ExcelExportType
{
    [Description("خروجی جهت سامانه سیاق")]
    SiaghType = 1,
    [Description("خروجی کلی جهت سامانه سیاق")]
    SiaghFullType = 2,
    [Description("خروجی تجمیعی جهت سامانه سیاق")]
    SiaghAggregationType = 3,
    [Description("خروجی جهت اداره مالیات")]
    TaxType = 4,
    [Description("خروجی کلی")]
    GeneralType = 5,
}
