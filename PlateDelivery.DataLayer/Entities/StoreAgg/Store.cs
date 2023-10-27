using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.DataLayer.Entities.StoreAgg;
public class Store : BaseEntity
{
    public Store(string plateText, string plateNumber, bool operationType)
    {
        PlateText = plateText;
        PlateNumber = plateNumber;
        OperationType = operationType;
    }

    public string PlateText { get; private set; }
    public string PlateNumber { get; private set; }
    public bool OperationType { get; private set; } // true = NEW and false = FAK
}
