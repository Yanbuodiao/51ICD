using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ServiceMenuModel
    {
        public int ServiceID { get; set; }
        public ServiceType ServiceType { get; set; }

        public List<ICDMenu> MenuList { get; set; }
    }
}
