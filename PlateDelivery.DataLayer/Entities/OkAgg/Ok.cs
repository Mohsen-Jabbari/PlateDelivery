using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.DataLayer.Entities.OkAgg;
public class Ok : BaseEntity
{
    public Ok(string contractOwnerId, string plateNumber, string chassisNumber, 
        string invoiceNumber, string requestDate, string plateStatus, string placeOfRemoval, 
        string dateOfRemoval, string detachDate, string chassisTrim)
    {
        ContractOwnerId = contractOwnerId;
        PlateNumber = plateNumber;
        ChassisNumber = chassisNumber;
        InvoiceNumber = invoiceNumber;
        RequestDate = requestDate;
        PlateStatus = plateStatus;
        PlaceOfRemoval = placeOfRemoval;
        DateOfRemoval = dateOfRemoval;
        DetachDate = detachDate;
        ChassisTrim = chassisTrim;
    }

    public string ContractOwnerId { get; private set; }
    public string PlateNumber { get; private set; }
    public string ChassisNumber { get; private set; }
    public string InvoiceNumber { get; private set; }
    public string RequestDate { get; private set; }
    public string PlateStatus { get; private set; }
    public string PlaceOfRemoval { get; private set; }
    public string DateOfRemoval { get; private set; }
    public string DetachDate { get; private set; }
    public string ChassisTrim { get; private set; }
}
