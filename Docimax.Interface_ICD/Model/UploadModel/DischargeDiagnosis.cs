using Docimax.Interface_ICD.Model.UploadModel.N041;
using System;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    /// <summary>
    /// 病案首页出院诊断实体类
    /// </summary>
    public class DischargeDiagnosis
    {
        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnosis { get; set; }
        /// <summary>
        /// 入院病情
        /// </summary>
        public AdmittingCondition AdmittingCondition { get; set; }
    }

    /// <summary>
    /// 病案首页手术和操作实体类
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// 手术、操作日期
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 手术、操作名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public Anesthesia? AnesthesiaType { get; set; }
        /// <summary>
        /// 手术愈合等级
        /// </summary>
        public IncisionHealingLevel? IncisionHealingLevel { get; set; }
        /// <summary>
        /// 手术愈合类别
        /// </summary>
        public IncisionHealingCategory? IncisionHealingCategory { get; set; }
        /// <summary>
        /// 手术等级
        /// </summary>
        public OperationLevel? OperationLevel { get; set; }
    }

    /// <summary>
    /// 手术操作详细记录实体类
    /// </summary>
    public class OperationDetail
    {
        /// <summary>
        /// 手术开始时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime OperationBegin { get; set; }
        /// <summary>
        /// 手术结束时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime OperationEnd { get; set; }
        /// <summary>
        /// 术前诊断
        /// </summary>
        public string PreoperativeDiagnosis { get; set; }
        /// <summary>
        /// 术中诊断
        /// </summary>
        public string IntraoperativeDiagnosis { get; set; }
        /// <summary>
        /// 手术名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 麻醉方法
        /// </summary>
        public string Anesthetic { get; set; }
        /// <summary>
        /// 手术经过、术中出现的情况及处理等
        /// </summary>
        public string Procedure { get; set; }
    }

    /// <summary>
    /// 入院记录
    /// </summary>
    public class AdmissionNote
    {
        /// <summary>
        /// 主诉
        /// </summary>
        public string ChiefComplaint { get; set; }
        /// <summary>
        /// 现病史
        /// </summary>
        public string HPI { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }
        /// <summary>
        /// 家族史
        /// </summary>
        public string FamilyHistory { get; set; }
        /// <summary>
        /// 个人史
        /// </summary>
        public string PersonalHistory { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string Allergies { get; set; }
        /// <summary>
        /// 体格检查
        /// </summary>
        public string PhysicalExamination { get; set; }
        /// <summary>
        /// 专科检查
        /// </summary>
        public string SpecialPE { get; set; }
        /// <summary>
        /// 辅助检查
        /// </summary>
        public string AssistantExamination { get; set; }
        /// <summary>
        /// 初步诊断
        /// </summary>
        public string PreliminaryDiagnosis { get; set; }
    }

    /// <summary>
    /// 出院记录
    /// </summary>
    public class DischargeRecord
    {
        /// <summary>
        /// 诊疗过程
        /// </summary>
        public string TreatmentProcedure { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string DischargeDiagnosis { get; set; }
        /// <summary>
        /// 出院情况
        /// </summary>
        public string DischargeCondition { get; set; }
        /// <summary>
        /// 出院医嘱
        /// </summary>
        public string DischargeOrder { get; set; }
    }

    /// <summary>
    /// 死亡记录实体类
    /// </summary>
    public class DeathNote
    {
        /// <summary>
        /// 死亡时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime DeathTime { get; set; }
        /// <summary>
        /// 诊疗经过(重点记录病情演变、抢救经过)
        /// </summary>
        public string TreatmentProcedure { get; set; }
        /// <summary>
        /// 死亡原因
        /// </summary>
        public string DeathCauses { get; set; }
        /// <summary>
        /// 死亡诊断
        /// </summary>
        public string DeathDiagnosis { get; set; }
    }

    /// <summary>
    /// 临时医嘱实体类
    /// </summary>
    public class STAT
    {
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 遗嘱内容
        /// </summary>
        public string OrderContent { get; set; }
    }

    /// <summary>
    /// 检查报告实体类
    /// </summary>
    public class InspectionReport
    {
        /// <summary>
        /// 检查项目名称
        /// </summary>
        public string InspectionName { get; set; }
        /// <summary>
        /// 检查结果
        /// </summary>
        public string InsepectionResult { get; set; }
        /// <summary>
        /// 报告时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime ReportTime { get; set; }
    }
}
