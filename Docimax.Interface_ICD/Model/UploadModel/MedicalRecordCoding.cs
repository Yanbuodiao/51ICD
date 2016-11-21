using Docimax.Interface_ICD.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    /// <summary>
    /// 病案编码实体类  病案号+出院时间+住院次数  形成唯一标记号
    /// </summary>
    public class MedicalRecordCoding
    {
        #region 唯一标记一份病案

        /// <summary>
        /// 病案号
        /// </summary>
        public string MedicalRecordNO { get; set; }
        /// <summary>
        /// 出院时间或者死亡时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime? DischargeDate { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public int AdmissionTimes { get; set; }

        #endregion

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public Identification PatientModel { get; set; }

        /// <summary>
        /// 门(急)诊诊断 
        /// </summary>
        public string AdmittingDiagnosis { get; set; }

        /// <summary>
        /// 入院时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime AdmittingTime { get; set; }

        /// <summary>
        /// 入院记录
        /// </summary>
        public AdmissionNote AdmissionNote { get; set; }

        /// <summary>
        /// 病案首页出院诊断集合
        /// </summary>
        public List<DischargeDiagnosis> DischargeDiagnosisList { get; set; }

        /// <summary>
        /// 病案首页手术和操作集合
        /// </summary>
        public List<Operation> ProceduresList { get; set; }

        /// <summary>
        /// 手术详细记录列表
        /// </summary>
        public List<OperationDetail> OperationDetailList { get; set; }

        /// <summary>
        /// 出院记录
        /// </summary>
        public DischargeRecord DischargeRecord { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public OrderTypeEnum OrderType { get; set; }

        /// <summary>
        /// 死亡记录
        /// </summary>
        public DeathNote DeathNote { get; set; }

        /// <summary>
        /// 临时医嘱List
        /// </summary>
        public List<STAT> STATList { get; set; }

        /// <summary>
        /// 检查报告List
        /// </summary>
        public List<InspectionReport> InspectionReports { get; set; }
    }
}
