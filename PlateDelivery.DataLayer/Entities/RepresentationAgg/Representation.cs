using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.RepresentationAgg;
public class Representation : BaseEntity
{
    public Representation(string representationCode, string validityDate, string brokerName,
        string brokerCode, string brokerTell, string brokerMobile, string extra,
        string brokerCodeR, bool isDelete)
    {
        Guard(representationCode, validityDate, brokerName, brokerCode, brokerTell,
            brokerMobile, extra, brokerCodeR);
        RepresentationCode = representationCode;
        ValidityDate = validityDate;
        BrokerName = brokerName;
        BrokerCode = brokerCode;
        BrokerTell = brokerTell;
        BrokerMobile = brokerMobile;
        Extra = extra;
        BrokerCodeR = brokerCodeR;
        IsDelete = isDelete;
    }

    public void Edit(string representationCode, string validityDate, string brokerName,
        string brokerCode, string brokerTell, string brokerMobile, string extra,
        string brokerCodeR, bool isDelete)
    {
        Guard(representationCode, validityDate, brokerName, brokerCode, brokerTell,
            brokerMobile, extra, brokerCodeR);
        RepresentationCode = representationCode;
        ValidityDate = validityDate;
        BrokerName = brokerName;
        BrokerCode = brokerCode;
        BrokerTell = brokerTell;
        BrokerMobile = brokerMobile;
        Extra = extra;
        BrokerCodeR = brokerCodeR;
        IsDelete = isDelete;
    }

    public void SetIsDeleteTrue()
    {
        IsDelete = true;
    }

    public void SetIsDeleteFalse()
    {
        IsDelete= false;
    }

    public string RepresentationCode { get; private set; }
    public string ValidityDate { get; private set; }
    public string BrokerName { get; private set; }
    public string BrokerCode { get; private set; }
    public string BrokerTell { get; private set; }
    public string BrokerMobile { get; private set; }
    public string Extra { get; private set; }
    public string BrokerCodeR { get; private set; }
    public bool IsDelete { get; private set; }

    public static void Guard(string representationCode, string validityDate, string brokerName,
        string brokerCode, string brokerTell, string brokerMobile, string extra,
        string brokerCodeR)
    {
        NullOrEmptyDataException.CheckString(representationCode, nameof(representationCode));
        NullOrEmptyDataException.CheckString(validityDate, nameof(validityDate));
        NullOrEmptyDataException.CheckString(brokerName, nameof(brokerName));
        NullOrEmptyDataException.CheckString(brokerCode, nameof(brokerCode));
        NullOrEmptyDataException.CheckString(brokerTell, nameof(brokerTell));
        NullOrEmptyDataException.CheckString(brokerMobile, nameof(brokerMobile));
        NullOrEmptyDataException.CheckString(extra, nameof(extra));
        NullOrEmptyDataException.CheckString(brokerCodeR, nameof(brokerCodeR));
    }
}
