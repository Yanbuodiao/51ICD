using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.RequestModel
{
    public class SyncRequestModel<T>
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
