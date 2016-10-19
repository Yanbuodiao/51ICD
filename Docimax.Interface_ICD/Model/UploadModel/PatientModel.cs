using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    public class 患者信息
    {
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string 民族 { get; set; }
        public string 婚姻状况 { get; set; }
        public string 职业 { get; set; }
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

        public 出院情况 _24小时内出院记录 { get; set; }
        public 死亡情况 _24小时内死亡记录 { get; set; }
    }

    public class 出院情况
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

}
