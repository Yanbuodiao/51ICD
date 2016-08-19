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

        public ServiceClaimOpereateType ClaimOpereateType { get; set; }

        public string ClaimValue { get; set; }
    }
}
