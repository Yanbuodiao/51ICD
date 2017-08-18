using Docimax.Interface_ICD.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    /// <summary>
    /// 病案实体基础类 病案号+出院时间（死亡时间）+住院次 唯一标记一份病案
    /// </summary>
    public class MedicalRecordBase : BaseModel
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonIgnore]
        public int CodeOrderID { get; set; }

        /// <summary>
        /// 该订单对应的平台唯一订单号
        /// </summary>
        public string PlatformOrderCode { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public ICDOrderState OrderStatus { get; set; }

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
        /// 订单类别
        /// </summary>
        [JsonIgnore]
        public OrderTypeEnum OrderType { get; set; }
        /// <summary>
        /// 订单json存储路径
        /// </summary>
        [JsonIgnore]
        public string MedicalRecordPath { get; set; }

        /// <summary>
        /// 诊断编码结果
        /// </summary>
        public List<DiagnosisCodeResult> DiagnosisCodeResultList { get; set; }
        /// <summary>
        /// 手术操作编码结果
        /// </summary>
        public List<OperationCodeResult> OperationCodeResultList { get; set; }
    }
}
