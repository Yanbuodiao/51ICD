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
        /// 本次请求唯一标记
        /// </summary>
        public string TicketID { get; set; }
        /// <summary>
        /// 上传内容实体
        /// </summary>
        public T UploadContent { get; set; }
        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string AsynNotifyURL { get; set; }

        /// <summary>
        /// 将本实例转化为需要加密的字典
        /// </summary>
        /// <returns>待加密的字典</returns>
        public IDictionary<string, string> ToEncryDictionary()
        {
            var result = new Dictionary<string, string>
            {
                {"TicketID",this.TicketID},
                {"UploadContent",JsonHelper.SerializeObject(this.UploadContent)},
            };
            if (!string.IsNullOrWhiteSpace(this.AsynNotifyURL))
            {
                result.Add("AsynNotifyURL", this.AsynNotifyURL);
            }
            return result;
        }

        /// <summary>
        /// 将字典转为实例
        /// </summary>
        /// <param name="dic">属性及属性值对应的字典</param>
        /// <returns>转化完后的实例</returns>
        public static UploadModel<T> DicToModel(Dictionary<string, string> dic)
        {
            var result = new UploadModel<T>
            {
                TicketID = dic["TicketID"],
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
