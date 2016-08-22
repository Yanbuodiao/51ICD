using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ServiceClaimModel
    {
        public int ServiceID { get; set; }
        public ServiceClaimType ClaimType { get; set; }

        private ServiceClaimOpereateType? claimOpereateType;
        public ServiceClaimOpereateType? ClaimOpereateType
        {
            get
            {
                if (claimOpereateType == null)
                {
                    claimOpereateType = ServiceClaimOpereateType.等于;
                }
                return claimOpereateType;
            }
            set
            {
                claimOpereateType = value;
            }
        }

        public string ClaimValue { get; set; }
    }
}
