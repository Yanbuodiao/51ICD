using Docimax.Interface_ICD.Enum;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model
{
    public class ServiceModel
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public bool DeleteFlag { get; set; }
        public CertificateState CertificateStatus { get; set; }
        public List<ServiceClaimModel> ServiceClaims { get; set; }
    }
}
