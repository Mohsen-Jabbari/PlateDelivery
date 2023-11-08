using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Core.Models
{
    public class PermissionViewModel : BaseDto
    {
        public string PermissionName { get; set; }
        public long? ParentId { get; set; } = null;
    }
}
