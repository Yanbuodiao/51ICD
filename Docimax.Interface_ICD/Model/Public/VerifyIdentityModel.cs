using Docimax.Interface_ICD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class VerifyIdentityModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户身证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户银行卡
        /// </summary>
        public string BankCardNO { get; set; }
        /// <summary>
        /// 发起申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 附件信息
        /// </summary>
        public List<ICDFile> FileList { get; set; }
        /// <summary>
        /// 认证状态
        /// </summary>
        public CertificateState CertificateFlag { get; set; }
    }
}
