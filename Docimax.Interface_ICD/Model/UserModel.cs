using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNO { get; set; }
        /// <summary>
        /// 实名认证状态与枚举Docimax.Interface_ICD.Enum.CertificateState的枚举值相对应
        /// </summary>
        public int? CertificationFlag { get; set; }
        /// <summary>
        /// 银行卡认证
        /// </summary>
        public int? BankCertificationFlag { get; set; }
        /// <summary>
        /// 所属单位，用户身份属性，与编码订单上传时需确认的医疗机构无关
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 所属组织ID
        /// </summary>
        public int? ORGID { get; set; }
        /// <summary>
        ///所属子组织ID
        /// </summary>
        public int? SubORGID { get; set; }
        /// <summary>
        /// 首次登陆
        /// </summary>
        public int? IsFirstLogin { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人用户ID
        /// </summary>
        public string CreateUserID { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModtifyTime { get; set; }
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public string LastModityUserID { get; set; }

        public string Password { private get; set; }

        public string HashPassWord { get { return CalculateMD5Hash(Password); } }

        [Timestamp]
        public byte[] LastModifyStamp { get; set; }

        private string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input + "Docimax");
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
