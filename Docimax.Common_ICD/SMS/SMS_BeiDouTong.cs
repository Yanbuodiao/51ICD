using Docimax.Interface_ICD.Configurations;
using Docimax.Interface_ICD.Interface;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common_ICD.SMS
{
    public class SMS_BeiDouTong : ISMS
    {
        public Task<bool> SendSMsAsync(SMSConfig sMSConfig, string sMSbody, string destination)
        {
            return Task.Run(() =>
            {
                return false;
            });
        }
    }
}
