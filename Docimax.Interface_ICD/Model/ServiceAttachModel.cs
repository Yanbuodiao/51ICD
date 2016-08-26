using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class ServiceAttachModel : BaseModel
    {
        public int ServiceID { get; set; }

        /// <summary>
        /// 上传时文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件目录
        /// </summary>
        public string FileURL { get; set; }

        /// <summary>
        /// 文件的ContentType,呈现文件时使用
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 附件类别
        /// </summary>
        public ServiceAttachType AttachType { get; set; }
        /// <summary>
        /// 附件在该组的排序值
        /// </summary>
        public int FileIndex { get; set; }
    }
}
