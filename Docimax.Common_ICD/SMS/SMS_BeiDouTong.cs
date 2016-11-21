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
        public Task SendSMsAsync(SMSConfig sMSConfig, string sMSbody, string destination)
        {
            return Task.Run(() =>
            {
                var postStr = string.Format("Id={0}&Name={1}&Psw={2}&Message={3}&Phone={4}&Timestamp={5}",
                    sMSConfig.Uid, sMSConfig.UName, sMSConfig.Pwd,
                    sMSbody, destination,
                    (DateTime.Now - new DateTime(1970, 1, 1)).Ticks);
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
