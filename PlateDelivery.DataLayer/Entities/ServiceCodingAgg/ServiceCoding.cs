using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
public class ServiceCoding : BaseEntity
{
    public ServiceCoding(string serviceName, string serviceFullName, string serviceCode, string codeLevel4,
        string codeLevel6, long certainId, string amount, bool includeTax)
    {
        ServiceName = serviceName;
        ServiceFullName = serviceFullName;
        ServiceCode = serviceCode;
        CodeLevel4 = codeLevel4;
        CodeLevel6 = codeLevel6;
        CertainId = certainId;
        Amount = amount;
        IncludeTax = includeTax;
    }

    public void Edit(string serviceName, string serviceFullName, string serviceCode, string codeLevel4, 
        string codeLevel6, long certainId, string amount, bool includeTax)
    {
        ServiceName = serviceName;
        ServiceFullName = serviceFullName;
        ServiceCode = serviceCode;
        CodeLevel4 = codeLevel4;
        CodeLevel6 = codeLevel6;
        CertainId = certainId;
        Amount = amount;
        IncludeTax = includeTax;
    }

    private ServiceCoding()
    {

    }

    public static void Guard(string ServiceName, string ServiceFullName, string ServiceCode, 
        string Amount)
    {
        NullOrEmptyDataException.CheckString(ServiceName, nameof(ServiceName));
        NullOrEmptyDataException.CheckString(ServiceFullName, nameof(ServiceFullName));
        NullOrEmptyDataException.CheckString(ServiceCode, nameof(ServiceCode));
        NullOrEmptyDataException.CheckString(Amount, nameof(Amount));
    }

    public string ServiceName { get; private set; }
    public string ServiceFullName { get; private set; }
    public string ServiceCode { get; private set; }
    public long CertainId { get; private set; }
    public string? CodeLevel4 { get; private set; }
    public string? CodeLevel6 { get; private set; }
    public string Amount { get; private set; }
    public bool IncludeTax { get; private set; }
}
