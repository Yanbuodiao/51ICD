using Docimax.Interface_ICD.Configurations;
using Docimax.Interface_ICD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common_ICD.SMS
{
    public class SMS_Ihuyi : ISMS
    {
        public Task SendSMsAsync(SMSConfig sMSConfig, string sMSbody, string destination)
        {
            return Task.Run(() =>
            {
                var postStr = string.Format("account={0}&password={1}&mobile={2}&content={3}", sMSConfig.UName, sMSConfig.Pwd,
                     destination,sMSbody);
                var postData = Encoding.UTF8.GetBytes(postStr);
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    client.Headers.Add("ContentLength", postData.Length.ToString());
                    var resultData = client.UploadData(sMSConfig.Server, "Post", postData);
                    var resultStr = Encoding.UTF8.GetString(resultData);
                }

            });
        }
    }
}
