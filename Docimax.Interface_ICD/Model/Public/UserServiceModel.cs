using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UserServiceModel : BaseModel
    {
        public int User_ServiceID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public CertificateState CertificateFlag { get; set; }
        public CertificateState UserCertificateFlag { get; set; }
        public ServiceModel Service { get; set; }
        public DateTime ApplyTime { get; set; }
        public DateTime PlatformAuditTime { get; set; }
        public string PlatformAuditUserID { get; set; }
        public DateTime PlatformAbendTime { get; set; }
        public string PlatformAbendUserID { get; set; }
    }
}
