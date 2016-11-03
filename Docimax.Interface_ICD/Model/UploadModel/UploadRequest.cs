using Newtonsoft.Json;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UploadRequest
    {
        /// <summary>
        /// 请求的机构Code  会进行提前授权
        /// </summary>
        public string ORGCode { get; set; }

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
                {"ORGCode",this.ORGCode},
                {"EncryptedRequest",this.EncryptedRequest},
            };
            return result;
        }
    }
}
