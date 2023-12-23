using System.ComponentModel;

namespace PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

public enum CertainCategory
{
    [Description("کد معین بانک")]
    Primary = 1,
    [Description("کد معین درآمد")]
    Income = 2,
    [Description("کد معین مالیات")]
    Tax = 3
}