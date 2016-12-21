using Docimax.Interface_ICD.Configurations;
using Docimax.Interface_ICD.Interface;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Docimax.Common_ICD.SMS
{
    public class SMS_Ihuyi : ISMS
    {
        public Task<bool> SendSMsAsync(SMSConfig sMSConfig, string sMSbody, string destination)
        {
            return Task.Run(() =>
            {
                var model = new SMS_IhuyiModel { SendTime = DateTime.Now, PhoneNum = destination };
                var postStr = string.Format("account={0}&password={1}&mobile={2}&content={3}",
                    sMSConfig.UName,
                    sMSConfig.Pwd,
                    destination, sMSbody);
                var postData = Encoding.UTF8.GetBytes(postStr);
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    client.Headers.Add("ContentLength", postData.Length.ToString());
                    var resultData = client.UploadData(sMSConfig.Server, "Post", postData);
                    var resultStr = Encoding.UTF8.GetString(resultData);
                    var xmlns = "http://106.ihuyi.cn/";
                    var xBase = XDocument.Parse(resultStr).Element(XName.Get("SubmitResult", xmlns));
                    model.Code = int.Parse(xBase.Element(XName.Get("code", xmlns)).Value);
                    model.RecievedTime = DateTime.Now;
                    model.Smsid =xBase.Element(XName.Get("smsid", xmlns)).Value;
                    model.Msg =xBase.Element(XName.Get("msg", xmlns)).Value;

                    //Todo  根据model 记录相关日志 为以后对账方便
                    if (model.Code == 2)
                    {
                        return true;
                    }
                    return false;
                }
            });
        }
    }
    public class SMS_IhuyiModel
    {
        public DateTime RecievedTime { get; set; }
        public string PhoneNum { get; set; }
        public DateTime SendTime { get; set; }
        public int Code { get; set; }
        public string Smsid { get; set; }
        public string Msg { get; set; }
    }

    public class SubmitResult
    {
        public int code { get; set; }
        public string smsid { get; set; }
        public string msg { get; set; }
    }
}
