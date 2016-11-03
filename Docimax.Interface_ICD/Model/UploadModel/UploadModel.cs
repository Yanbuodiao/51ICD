using Docimax.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UploadModel<T> where T:class
    {
        /// <summary>
        /// 幂等操作的唯一标记（一个TicketID必须保证业务实体的一致性）
        /// </summary>
        public string TicketID { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        ///发送请求的时间，格式"yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// 获得请求的时间
        /// </summary>
        [JsonIgnore]
        public DateTime RecieveTime { get; set; }
        /// <summary>
        /// 上传内容实体
        /// </summary>
        public T UploadContent { get; set; }
        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string AsynNotifyURL { get; set; }

        public IDictionary<string, string> ToEncryDictionary()
        {
            var result = new Dictionary<string, string>
            {
                {"TicketID",this.TicketID},
                {"Version",this.Version},
                {"RequestTime",this.RequestTime.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                {"UploadContent",JsonHelper.SerializeObject(this.UploadContent)},
            };
            if (!string.IsNullOrWhiteSpace(this.AsynNotifyURL))
            {
                result.Add("AsynNotifyURL", this.AsynNotifyURL);
            }
            return result;
        }

        public static UploadModel<T> DicToModel(Dictionary<string, string> dic)
        {
            var result = new UploadModel<T>
            {
                TicketID = dic["TicketID"],
                Version = dic["Version"],
                RequestTime = DateTime.Parse(dic["RequestTime"]),
                UploadContent = JsonHelper.DeserializeObject<T>(dic["UploadContent"]),
            };
            if (dic.Keys.Any(e => e == "AsynNotifyURL"))
            {
                result.AsynNotifyURL = dic["AsynNotifyURL"];
            }
            return result;
        }
    }
}
