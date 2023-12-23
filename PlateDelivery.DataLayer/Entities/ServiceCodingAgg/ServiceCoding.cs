using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;
using PlateDelivery.DataLayer.Entities.CertainAgg.Enums;

namespace PlateDelivery.DataLayer.Entities.ServiceCodingAgg;
public class ServiceCoding : BaseEntity
{
    public ServiceCoding(string serviceName, string serviceFullName, string serviceCode, string codeLevel4, 
        string codeLevel6, long certainId)
    {
        ServiceName = serviceName;
        ServiceFullName = serviceFullName;
        ServiceCode = serviceCode;
        CodeLevel4 = codeLevel4;
        CodeLevel6 = codeLevel6;
        CertainId = certainId;
    }

    public void Edit(string serviceName, string serviceFullName, string serviceCode, string codeLevel4, 
        string codeLevel6, long certainId)
    {
        ServiceName = serviceName;
        ServiceFullName = serviceFullName;
        ServiceCode = serviceCode;
        CodeLevel4 = codeLevel4;
        CodeLevel6 = codeLevel6;
        CertainId = certainId;
    }

    private ServiceCoding()
    {

    }

    public static void Guard(string ServiceName, string ServiceFullName, string ServiceCode, string CodeLevel4, string CodeLevel6)
    {
        NullOrEmptyDataException.CheckString(ServiceName, nameof(ServiceName));
        NullOrEmptyDataException.CheckString(ServiceFullName, nameof(ServiceFullName));
        NullOrEmptyDataException.CheckString(ServiceCode, nameof(ServiceCode));
        NullOrEmptyDataException.CheckString(CodeLevel4, nameof(CodeLevel4));
        NullOrEmptyDataException.CheckString(CodeLevel6, nameof(CodeLevel6));
    }

    public string ServiceName { get; private set; }
    public string ServiceFullName { get; private set; }
    public string ServiceCode { get; private set; }
    public long CertainId { get; private set; }
    public string CodeLevel4 { get; private set; }
    public string CodeLevel6 { get; private set; }
}
