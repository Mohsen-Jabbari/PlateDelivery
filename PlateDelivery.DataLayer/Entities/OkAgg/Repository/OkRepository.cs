using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.OkAgg.Repository;

internal class OkRepository : BaseRepository<Ok>, IOkRepository
{
    public OkRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public bool ImportOkFile()
    {
        var readcsv = File.ReadAllText("E:\\Pelak\\Ok.txt");
        string[] csvfilerecord = readcsv.Split('\n');
        foreach (var row in csvfilerecord)
        {
            if (!string.IsNullOrEmpty(row))
            {
                row.Replace("\r", "");
                var cells = row.Split(',');
                if (!Context.Oks.Any(o => o.ContractOwnerId == cells[0] && o.PlateNumber == cells[1] && o.ChassisNumber == cells[2] && o.InvoiceNumber == cells[3]))
                {
                    var reccord = new Ok(cells[0], cells[1], cells[2], cells[3], cells[4], cells[5],
                    cells[6], cells[7], cells[8], cells[2][..6]);
                    Context.Oks.Add(reccord);
                }
            }
        }
        Context.SaveChanges();
        return true;
    }
}