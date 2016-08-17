using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UserServiceApplyModel : BaseModel
    {
        public string UserID { get; set; }
        public ServiceModel Service { get; set; }
        public CertificateState CertificateStatus { get; set; }
    }
}
