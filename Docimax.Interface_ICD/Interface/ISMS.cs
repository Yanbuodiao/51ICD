using Docimax.Interface_ICD.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface ISMS
    {
        Task SendSMsAsync(SMSConfig sMSConfig, string sMSbody, string destination);
    }
}
