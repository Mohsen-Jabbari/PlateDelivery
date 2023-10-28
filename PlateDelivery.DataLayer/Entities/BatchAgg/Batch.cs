using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.DataLayer.Entities.BatchAgg;
public class Batch : BaseEntity
{
    public Batch(string contractOwnerId, string chassisTrim, long agencyId, 
        string ownerName, long agencyPlaceId, string requestDate)
    {
        ContractOwnerId = contractOwnerId;
        ChassisTrim = chassisTrim;
        AgencyId = agencyId;
        OwnerName = ownerName;
        AgencyPlaceId = agencyPlaceId;
        RequestDate = requestDate;
    }

    public string ContractOwnerId { get; private set; }
    public string ChassisTrim { get; private set; }
    public long AgencyId { get; private set; }
    public string OwnerName { get; private set; }
    public long AgencyPlaceId { get; private set; }
    public string RequestDate { get; private set; }
}
