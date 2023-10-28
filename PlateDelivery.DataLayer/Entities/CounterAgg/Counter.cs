using PlateDelivery.Common.CommonClasses;

namespace PlateDelivery.DataLayer.Entities.CounterAgg;
public class Counter : BaseEntity
{
    public long PlateCount { get; private set; }

    public Counter(long plateCount)
    {
        PlateCount = plateCount;
    }
}
