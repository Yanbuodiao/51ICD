using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class SecurityPhoneModel
    {
        public string PhoneNumber { get; set; }
        public DateTime LastSendTime { get; set; }
        public string SourceIP { get; set; }
        public string UserID { get; set; }
    }
}
