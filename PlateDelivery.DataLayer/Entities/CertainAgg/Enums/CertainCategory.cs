using System.ComponentModel;

namespace PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

public enum CertainCategory
{
    [Description("نوع 1")]
    Primary = 1,
    [Description("نوع 2")]
    Income = 2,
    [Description("نوع 3")]
    Tax = 3
}