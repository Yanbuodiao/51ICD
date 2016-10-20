using System.Collections.Generic;

namespace Docimax.Interface_ICD.Model.UploadModel
{
    /// <summary>
    /// 所有内容中，不出现患者姓名，身份证号，联系方式，医生姓名
    /// </summary>
    public class CodeModel_N041
    {
        #region 病案首页相关字段

        /// <summary>
        /// 机构名称
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 医疗付款方式
        /// </summary>
        public string YLFKFS { get; set; }
        /// <summary>
        /// 健康卡号
        /// </summary>
        public string JKKH { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public string ZYCS { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string BAH { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }
        /// <summary>
        /// 出生日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string CSRQ { get; set; }
        /// <summary>
        /// 年龄   
        /// </summary>
        public int NL { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string GJ { get; set; }
        /// <summary>
        /// (年龄不足1周岁的)年龄(月)
        /// </summary>
        public int BZYZSNL { get; set; }
        /// <summary>
        /// 新生儿出生体重(克)
        /// </summary>
        public int XSECSTZ { get; set; }
        /// <summary>
        /// 新生儿入院体重(克）
        /// </summary>
        public int XSERYTZ { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string CSD { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string GG { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string ZY { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        public string HY { get; set; }
        /// <summary>
        /// 入院途径
        /// </summary>
        public string RYTJ { get; set; }
        /// <summary>
        /// 入院时间  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string RYSJ { get; set; }
        /// <summary>
        /// 入院时间 时
        /// </summary>
        public int RYSJS { get; set; }
        /// <summary>
        /// 入院科别
        /// </summary>
        public string RYKB { get; set; }
        /// <summary>
        /// 入院病房
        /// </summary>
        public string RYBF { get; set; }
        /// <summary>
        /// 转科科别
        /// </summary>
        public string ZKKB { get; set; }
        /// <summary>
        /// 出院时间  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string CYSJ { get; set; }
        /// <summary>
        /// 出院时间 时
        /// </summary>
        public int CYSJS { get; set; }
        /// <summary>
        /// 出院科别
        /// </summary>
        public string CYKB { get; set; }
        /// <summary>
        /// 出院病房
        /// </summary>
        public string CYBF { get; set; }
        /// <summary>
        /// 实际住院(天)
        /// </summary>
        public string SJZYTS { get; set; }
        /// <summary>
        /// 门(急)诊诊断
        /// </summary>
        public string MZZD { get; set; }
        /// <summary>
        /// 门诊诊断编码
        /// </summary>
        public string JBBM { get; set; }

        #region 出院诊断

        /// <summary>
        /// 主要诊断
        /// </summary>
        public string ZYZD { get; set; }
        /// <summary>
        /// 入院病情
        /// </summary>
        public string RYBQ { get; set; }
        /// <summary>
        /// 其他诊断1
        /// </summary>
        public string QTZD1 { get; set; }
        /// <summary>
        /// 入院病情1
        /// </summary>
        public string RYBQ1 { get; set; }
        /// <summary>
        /// 其他诊断2
        /// </summary>
        public string QTZD2 { get; set; }
        /// <summary>
        /// 入院病情2
        /// </summary>
        public string RYBQ2 { get; set; }
        /// <summary>
        /// 其他诊断3
        /// </summary>
        public string QTZD3 { get; set; }
        /// <summary>
        /// 入院病情3
        /// </summary>
        public string RYBQ3 { get; set; }
        /// <summary>
        /// 其他诊断4
        /// </summary>
        public string QTZD4 { get; set; }
        /// <summary>
        /// 入院病情4
        /// </summary>
        public string RYBQ4 { get; set; }
        /// <summary>
        /// 其他诊断5
        /// </summary>
        public string QTZD5 { get; set; }
        /// <summary>
        /// 入院病情5
        /// </summary>
        public string RYBQ5 { get; set; }
        /// <summary>
        /// 其他诊断6
        /// </summary>
        public string QTZD6 { get; set; }
        /// <summary>
        /// 入院病情6
        /// </summary>
        public string RYBQ6 { get; set; }
        /// <summary>
        /// 其他诊断7
        /// </summary>
        public string QTZD7 { get; set; }
        /// <summary>
        /// 入院病情7
        /// </summary>
        public string RYBQ7 { get; set; }
        /// <summary>
        /// 其他诊断8
        /// </summary>
        public string QTZD8 { get; set; }
        /// <summary>
        /// 入院病情8
        /// </summary>
        public string RYBQ8 { get; set; }
        /// <summary>
        /// 其他诊断9
        /// </summary>
        public string QTZD9 { get; set; }
        /// <summary>
        /// 入院病情9
        /// </summary>
        public string RYBQ9 { get; set; }
        /// <summary>
        /// 其他诊断10
        /// </summary>
        public string QTZD10 { get; set; }
        /// <summary>
        /// 入院病情10
        /// </summary>
        public string RYBQ10 { get; set; }
        /// <summary>
        /// 其他诊断11
        /// </summary>
        public string QTZD11 { get; set; }
        /// <summary>
        /// 入院病情11
        /// </summary>
        public string RYBQ11 { get; set; }
        /// <summary>
        /// 其他诊断12
        /// </summary>
        public string QTZD12 { get; set; }
        /// <summary>
        /// 入院病情12
        /// </summary>
        public string RYBQ12 { get; set; }
        /// <summary>
        /// 其他诊断13
        /// </summary>
        public string QTZD13 { get; set; }
        /// <summary>
        /// 入院病情13
        /// </summary>
        public string RYBQ13 { get; set; }
        /// <summary>
        /// 其他诊断14
        /// </summary>
        public string QTZD14 { get; set; }
        /// <summary>
        /// 入院病情14
        /// </summary>
        public string RYBQ14 { get; set; }
        /// <summary>
        /// 其他诊断15
        /// </summary>
        public string QTZD15 { get; set; }
        /// <summary>
        /// 入院病情15
        /// </summary>
        public string RYBQ15 { get; set; }
        #endregion

        /// <summary>
        /// 中毒外部原因
        /// </summary>
        public string WBYY { get; set; }
        /// <summary>
        /// 病理诊断
        /// </summary>
        public string BLZD { get; set; }
        /// <summary>
        /// 病理号
        /// </summary>
        public string BLH { get; set; }
        /// <summary>
        /// 药物过敏
        /// </summary>
        public string YWGM { get; set; }
        /// <summary>
        /// 过敏药物疾病
        /// </summary>
        public string GMYW { get; set; }
        /// <summary>
        /// 死亡患者尸检
        /// </summary>
        public string SWHZSJ { get; set; }
        /// <summary>
        /// 血型
        /// </summary>
        public string XX { get; set; }
        /// <summary>
        /// Rh血型
        /// </summary>
        public string RH { get; set; }

        #region 手术

        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ1 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB1 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC1 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ1 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB1 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS1 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ2 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB2 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC2 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ2 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB2 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS2 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ3 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB3 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC3 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ3 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB3 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS3 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ4 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB4 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC4 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ4 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB4 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS4 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ5 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB5 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC5 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ5 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB5 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS5 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ6 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB6 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC6 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ6 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB6 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS6 { get; set; }
        /// <summary>
        /// 手术及操作日期  格式为：YYYYMMDD，例如：20131125
        /// </summary>
        public string SSJCZRQ7 { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        public string SSJB7 { get; set; }
        /// <summary>
        /// 手术及操作名称
        /// </summary>
        public string SSJCZMC7 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string QKDJ7 { get; set; }
        /// <summary>
        /// 切口愈合类别
        /// </summary>
        public string QKYHLB7 { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string MZFS7 { get; set; }

        #endregion

        /// <summary>
        /// 离院方式
        /// </summary>
        public string LYFS { get; set; }
        /// <summary>
        /// 是否有出院31天内再住院计划手术情况
        /// </summary>
        public string SFZZYJH { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        public string MD { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：天
        /// </summary>
        public int RYQ_T { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：小时
        /// </summary>
        public int RYQ_XS { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：分
        /// </summary>
        public int RYQ_F { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：天
        /// </summary>
        public int RYH_T { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：小时
        /// </summary>
        public int RYH_XS { get; set; }
        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：分
        /// </summary>
        public int RYH_F { get; set; }

        #region 费用信息

        /// <summary>
        /// 住院费用(元)：总费用  2位小数
        /// </summary>
        public decimal ZFY { get; set; }
        /// <summary>
        /// 自付金额  2位小数
        /// </summary>
        public decimal ZFJE { get; set; }
        /// <summary>
        /// 综合医疗服务类：(1)一般医疗服务费 2位小数
        /// </summary>
        public decimal YLFUF { get; set; }
        /// <summary>
        /// 一般治疗操作费 2位小数
        /// </summary>
        public decimal ZLCZF { get; set; }
        /// <summary>
        /// 护理费住院费 2位小数
        /// </summary>
        public decimal HLF { get; set; }
        /// <summary>
        /// 其他费用 2位小数
        /// </summary>
        public decimal QTFY { get; set; }
        /// <summary>
        /// 诊断类：(5)病理诊断费 2位小数
        /// </summary>
        public decimal BLZDF { get; set; }
        /// <summary>
        /// 实验室诊断费  2位小数
        /// </summary>
        public decimal SYSZDF { get; set; }
        /// <summary>
        /// 影像学诊断费  2位小数
        /// </summary>
        public decimal YXXZDF { get; set; }
        /// <summary>
        /// 临床诊断项目费 2位小数
        /// </summary>
        public decimal LCZDXMF { get; set; }
        /// <summary>
        /// 治疗类：(9)非手术治疗项目费  2位小数
        /// </summary>
        public decimal FSSZLXMF { get; set; }
        /// <summary>
        /// 临床物理治疗费  2位小数
        /// </summary>
        public decimal WLZLF { get; set; }
        /// <summary>
        /// 手术治疗费  2位小数
        /// </summary>
        public decimal SSZLF { get; set; }
        /// <summary>
        /// 麻醉费  2位小数
        /// </summary>
        public decimal MAF { get; set; }
        /// <summary>
        /// 手术费  2位小数
        /// </summary>
        public decimal SSF { get; set; }
        /// <summary>
        /// 康复类：(11)康复费  2位小数
        /// </summary>
        public decimal KFF { get; set; }
        /// <summary>
        /// 中医类:(12)中医治疗费  2位小数
        /// </summary>
        public decimal ZYZLF { get; set; }
        /// <summary>
        /// 西药类:(13)西药费  2位小数
        /// </summary>
        public decimal XYF { get; set; }
        /// <summary>
        /// 抗菌药物费  2位小数
        /// </summary>
        public decimal KJYWF { get; set; }
        /// <summary>
        /// 中药类:(14)中成药费  2位小数
        /// </summary>
        public decimal ZCYF { get; set; }
        /// <summary>
        /// 中草药费  2位小数
        /// </summary>
        public decimal ZCYF1 { get; set; }
        /// <summary>
        /// 血液和血液制品类:(16)血费  2位小数
        /// </summary>
        public decimal XF { get; set; }
        /// <summary>
        /// 白蛋白类制品费  2位小数
        /// </summary>
        public decimal BDBLZPF { get; set; }
        /// <summary>
        /// 球蛋白类制品费  2位小数
        /// </summary>
        public decimal QDBLZPF { get; set; }
        /// <summary>
        /// 凝血因子类制品费  2位小数
        /// </summary>       
        public decimal NXYZLZPF { get; set; }
        /// <summary>
        /// 细胞因子类制品费  2位小数
        /// </summary>
        public decimal XBYZLZPF { get; set; }
        /// <summary>
        /// 耗材类:(21)检查用一次性医用材料费  2位小数
        /// </summary>
        public decimal HCYYCLF { get; set; }
        /// <summary>
        /// (22)治疗用一次性医用材料费  2位小数
        /// </summary>
        public decimal YYCLF { get; set; }
        /// <summary>
        /// (23)手术用一次性医用材料费
        /// </summary>
        public decimal YCXYYCLF { get; set; }
        /// <summary>
        /// 其他类：(24)其他费
        /// </summary>
        public decimal QTF { get; set; }

        #endregion

        #endregion

        #region 入院记录
        public 入院情况 入院记录 { get; set; }

        #endregion

        #region 病程记录

        public string 首次病程记录 { get; set; }
        public string 日常病程记录 { get; set; }
        public string 上级医师查房记录 { get; set; }
        public string 疑难病例讨论记录 { get; set; }
        public string 交接班记录 { get; set; }
        public string 转科记录 { get; set; }
        public string 阶段小结 { get; set; }

        #endregion

        #region 术前讨论记录

        public string 术前讨论记录 { get; set; }

        #endregion

        #region  术前小结

        public string 术前小结 { get; set; }

        #endregion

        #region 麻醉术前访视记录

        public string 麻醉前访视记录 { get; set; }

        #endregion

        #region 手术安全核查记录

        public string 手术安全检查记录 { get; set; }

        #endregion

        #region 手术清点记录

        public string 手术清点记录 { get; set; }

        #endregion

        #region 麻醉记录

        public string 麻醉记录 { get; set; }

        #endregion

        #region 手术记录

        public string 手术记录 { get; set; }

        #endregion

        #region 麻醉术后访视记录

        public string 麻醉术后访视记录 { get; set; }

        #endregion

        #region 术后病程记录

        public string 术后首次病程记录 { get; set; }

        #endregion

        #region 出院记录

        public 出院情况记录 出院记录 { get; set; }

        #endregion

        #region 死亡记录

        public 死亡情况 死亡记录 { get; set; }

        #endregion

        #region 死亡病例讨论记录

        public string 死亡病例讨论记录 { get; set; }

        #endregion

        #region 有创诊疗操作记录

        public string 有创诊疗操作记录 { get; set; }

        #endregion

        #region 会诊记录

        public string 会诊记录 { get; set; }

        #endregion

        #region 病理资料

        public string 病理资料 { get; set; }

        #endregion

        #region 辅助检查报告单

        #endregion

        #region 医学影像检查资料

        #endregion

        #region 抢救记录

        public string 抢救记录 { get; set; }

        #endregion

        #region 体温单

        #endregion

        #region 医嘱单
        public string 医嘱单 { get; set; }

        #endregion

        #region 病重（病危）患者护理记录

        public string 病重病危患者护理记录 { get; set; }

        #endregion

        public List<MedicalFileModel> 医学文件 { get; set; }
    }
}
