using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    public class SyncResponse<T>
    {
        /// <summary>
        /// 回复Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 回复描述
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 同步通知内容
        /// </summary>
        public T ResponseContent { get; set; }
    }
}
