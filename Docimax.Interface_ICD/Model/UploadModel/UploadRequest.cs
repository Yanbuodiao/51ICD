using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UploadRequest
    {
        /// <summary>
        /// 请求机构的授权Code
        /// </summary>
        public string AUTHCode { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        ///发送请求的时间，格式"yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// 获得请求的时间
        /// </summary>
        [JsonIgnore]
        public DateTime RecieveTime { get; set; }

        /// <summary>
        /// 请求内容的加密内容
        /// </summary>
        public string EncryptedRequest { get; set; }

        /// <summary>
        /// 数字签名
        /// </summary>
        public string Sign { get; set; }

        public IDictionary<string, string> ToSignDictionary()
        {
            var result = new Dictionary<string, string>
            {
                {"AUTHCode",this.AUTHCode},
                {"Version",this.Version},
                {"RequestTime",this.RequestTime.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                {"EncryptedRequest",this.EncryptedRequest},
            };
            return result;
        }
    }

    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter() { base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff"; }
    }
}
