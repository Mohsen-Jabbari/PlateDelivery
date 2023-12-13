using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.CertainAgg;
public class Certain : BaseEntity
{
    public Certain(string certainName, string certainCode, CertainCategory category)
    {
        Guard(certainName, certainCode);
        CertainName = certainName;
        CertainCode = certainCode;
        Category = category;
    }

    public void Edit(string certainName, string certainCode, CertainCategory category)
    {
        Guard(certainName, certainCode);
        CertainName = certainName;
        CertainCode = certainCode;
        Category = category;
    }

    private Certain()
    {

    }

    public static void Guard(string CertainName, string CertainCode)
    {
        NullOrEmptyDataException.CheckString(CertainName, nameof(CertainName));
        NullOrEmptyDataException.CheckString(CertainCode, nameof(CertainCode));
    }

    public string CertainName { get; private set; } //عنوان کد معین
    public string CertainCode { get; private set; } //عدد کد معین
    public CertainCategory Category { get; private set; }
}
