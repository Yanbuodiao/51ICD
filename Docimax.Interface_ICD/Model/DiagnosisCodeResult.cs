
namespace Docimax.Interface_ICD.Model
{
    public class DiagnosisCodeResult : BaseModel
    {
        /// <summary>
        /// 编码结果ID
        /// </summary>
        public int CodeResultID { get; set; }
        /// <summary>
        /// 所属订单ID
        /// </summary>
        public int CodeOrderID { get; set; }
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
        public string ICDCode { get; set; }
        /// <summary>
        /// 诊断编码对应的名称
        /// </summary>
        public string ICDName { get; set; }
        /// <summary>
        /// 页面带回的编码和编码对应的名称
        /// </summary>
        public string DisplayText { get; set; }
        public string Description { get; set; }
    }

    public class OperationCodeResult : BaseModel
    {
        /// <summary>
        /// 编码结果ID
        /// </summary>
        public int CodeResultID { get; set; }
        /// <summary>
        /// 所属订单ID
        /// </summary>
        public int CodeOrderID { get; set; }
        /// <summary>
        /// 手术、操作名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 主要手术/操作是0 其他手术/操作 依次累加
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 手术或操作编码
        /// </summary>
        public string ICDCode { get; set; }
        /// <summary>
        /// 手术或操作编码对应的名称
        /// </summary>
        public string ICDName { get; set; }
        /// <summary>
        /// 页面带回的编码和编码对应的名称
        /// </summary>
        public string DisplayText { get; set; }
        public string Description { get; set; }
    }
}
