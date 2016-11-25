using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class DiagnosisCodeResult
    {
        /// <summary>
        /// 临床诊断名称
        /// </summary>
        public string Diagnosis { get; set; }
        /// <summary>
        /// 主要诊断是0 其他诊断/合并症/并发诊断 依次累加
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnosisCode { get; set; }
    }

    public class OperationCodeResult
    {
        /// <summary>
        /// 手术、操作名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 主要手术/操作是0 其他手术/操作 依次累加
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 操作或者手术编码
        /// </summary>
        public string OperationCode { get; set; }
    }
}
