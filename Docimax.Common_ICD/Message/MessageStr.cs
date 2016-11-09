using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common_ICD.Message
{
    /// <summary>
    /// 平台回应信息字典
    /// </summary>
    public static class MessageStr
    {
        #region 正常请求回复码

        /// <summary>
        /// 0000:收到请求
        /// </summary>
        public const string SyncStr = "0000:收到请求";
        /// <summary>
        /// 1000:处理成功
        /// </summary>
        public const string AsynSre = "1000:处理成功";

        #endregion

        #region 基础技术错误码

        /// <summary>
        /// E001:Json格式不正确
        /// </summary>
        public const string JsonDeserializeError = "E001:Json格式不正确";
        /// <summary>
        /// E002:验证签名失败
        /// </summary>
        public const string CheckSignFail = "E002:验证签名失败";
        /// <summary>
        /// E003:解密失败
        /// </summary>
        public const string EncryptFail = "E003:解密失败";
        /// <summary>
        /// E004:未找到请求体
        /// </summary>
        public const string RequestNull = "E004:未找到请求体";
        /// <summary>
        /// E005:未找到业务内容
        /// </summary>
        public const string ContentNull = "E005:未找到业务内容";
        /// <summary>
        /// E006:未找到本次唯一请求标记
        /// </summary>
        public const string TicketNull = "E006:未找到本次唯一请求标记";

        #endregion

        #region 基础业务错误码
        /// <summary>
        /// B001:未找到版本号
        /// </summary>
        public const string VersionNull = "B001:未找到版本号";
        /// <summary>
        /// B002:未授权
        /// </summary>
        public const string OrganizationNull = "B002:未授权";
        /// <summary>
        /// B003:请求过期
        /// </summary>
        public const string RecieveTimeExpire = "B003:请求过期";

        #endregion
    }
}
