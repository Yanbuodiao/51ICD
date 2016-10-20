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
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求参数的签名串
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        ///发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// 获得请求的时间
        /// </summary>
        public DateTime RecieveTime { get; set; }
        /// <summary>
        /// 上传内容实体
        /// </summary>
        public T UploadContent { get; set; }
    }
}
