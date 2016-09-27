using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    /// <summary>
    /// 编码订单上传内容
    /// </summary>
    public class CodeModel_HQMS
    {
        /// <summary>
        /// 医疗付款方式 1
        /// </summary>
        public string P1 { get; set; }
        /// <summary>
        /// 住院次数 3
        /// </summary>
        public int P2 { get; set; }
        /// <summary>
        /// 病案号 20
        /// </summary>
        public string P3 { get; set; }
        /// <summary>
        /// 性别 1
        /// </summary>
        public string P5 { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime P6 { get; set; }
        /// <summary>
        /// 患者入院年龄  3
        /// </summary>
        public int P7 { get; set; }
        /// <summary>
        /// 婚姻状况 1
        /// </summary>
        public string P8 { get; set; }
        /// <summary> 
        /// 职业 2
        /// </summary>
        public string P9 { get; set; }
        /// <summary>
        /// 民族 20
        /// </summary>
        public string P11 { get; set; }
        /// <summary>
        /// 国籍 40
        /// </summary>
        public string P12 { get; set; }
    }
}
