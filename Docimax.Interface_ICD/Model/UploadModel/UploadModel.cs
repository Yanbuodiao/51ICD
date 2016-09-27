using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    public class UploadModel<T>
    {
        /// <summary>
        /// 请求的机构Code
        /// </summary>
        public string ORGCode { get; set; }
        /// <summary>
        /// 幂等操作的唯一标记（一个TicketID必须保证业务实体的一致性）
        /// </summary>
        public string TicketID { get; set; }
        /// <summary>
        /// 签名结果
        /// </summary>
        public string SignResult { get; set; }
        /// <summary>
        /// 发起请求的时间
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// 获得请求的时间
        /// </summary>
        public DateTime RecieveTime { get; set; }
        /// <summary>
        /// 请求体
        /// </summary>
        public T UploadContent { get; set; }
    }
}
