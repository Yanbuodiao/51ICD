using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UserAvailableServiceModel
    {
        public string UserID { get; set; }
        public List<ServiceMenuModel> AvaliableServices { get; set; }
    }
}
