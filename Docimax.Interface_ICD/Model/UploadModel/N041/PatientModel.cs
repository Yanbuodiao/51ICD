﻿
using Docimax.Interface_ICD.Model.UploadModel.N041;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model.UploadModel.N041
{
    public class 患者信息
    {
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 入院时年龄 
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public RaceEnum Race { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public MarriageEnum Marriage { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 体重  ep：
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }
    }

    public class 入院情况
    {
        public 患者信息 患者一般情况 { get; set; }
        public string 主诉 { get; set; }
        public string 现病史 { get; set; }
        public string 既往史 { get; set; }
        public string 个人史 { get; set; }
        public string 婚育史 { get; set; }
        public string 月经史 { get; set; }
        public string 家族史 { get; set; }
        public string 体格检查 { get; set; }
        public string 专科情况 { get; set; }
        public string 辅助检查 { get; set; }
        public string 初步诊断 { get; set; }
        public DateTime 入院时间 { get; set; }

        public List<入院情况> 再次或多次入院记录 { get; set; }

        public 出院情况记录 _24小时内出院记录 { get; set; }
        public 死亡情况 _24小时内死亡记录 { get; set; }
    }

    public class 出院情况记录
    {
        public DateTime 出院时间 { get; set; }
        public string 入院诊断 { get; set; }
        public string 诊疗经过 { get; set; }
        public string 出院诊断 { get; set; }
        public string 出院情况 { get; set; }
        public string 出院医嘱 { get; set; }
    }

    public class 死亡情况
    {
        public DateTime 死亡时间 { get; set; }
        public string 入院诊断 { get; set; }
        public string 诊疗经过 { get; set; }
        public string 死亡原因 { get; set; }
        public string 死亡诊断 { get; set; }
    }

    public class 手术情况
    {
        [JsonProperty(Order = 1)]
        public string 术前讨论记录 { get; set; }

        public string 术前小结 { get; set; }

        public string 麻醉前访视记录 { get; set; }

        public string 手术安全检查记录 { get; set; }

        public string 手术清点记录 { get; set; }

        public string 麻醉记录 { get; set; }

        public string 手术记录 { get; set; }

        public string 麻醉术后访视记录 { get; set; }

        public string 术后首次病程记录 { get; set; }
    }

    public class 有创诊疗记录
    {
        public string 术前讨论记录 { get; set; }

        public string 术前小结 { get; set; }

        public string 麻醉前访视记录 { get; set; }

        public string 手术安全检查记录 { get; set; }

        public string 手术清点记录 { get; set; }

        public string 麻醉记录 { get; set; }

        public string 有创诊疗操作记录 { get; set; }

        public string 麻醉术后访视记录 { get; set; }

        public string 术后首次病程记录 { get; set; }
    }

    public class 出院诊断详情
    {
        public string 临床诊断 { get; set; }

        public AdmittingCondition 入院病情 { get; set; }

        public int? Index { get; set; }
    }

    public class 手术概要情况
    {
        public DateTime 手术操作日期 { get; set; }
        public 手术级别 级别 { get; set; }
        public string 手术及操作名称 { get; set; }
        public string 切口愈合等级 { get; set; }
        public 切口愈合类别 切口愈合类别 { get; set; }

        public Anesthesia 麻醉方式 { get; set; }
    }
}
