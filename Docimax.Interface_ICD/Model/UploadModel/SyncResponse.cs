using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    public class SyncResponse<T>
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public string Sub_Code { get; set; }
        public string Sub_Msg { get; set; }
        /// <summary>
        /// 同步通知内容
        /// </summary>
        public T ResponseContent { get; set; }
    }
}
