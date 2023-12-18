using PlateDelivery.Core.Models.Provinces;
using PlateDelivery.Core.Security;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Core.Services.Provinces;
public interface IProvinceService
{
    long CreateProvince(CreateProvinceViewModel model);
    bool EditProvince(EditProvinceViewModel model);
    bool DeleteProvince(long Id);

    ProvincesViewModel GetProvinces(int pageId = 1, int take = 50, string filterByProvince = "");
}
