using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common_ICD.Message
{
    public static class MessageStr
    {
        #region 正常请求回复码

        public const string SyncStr = "0000:收到请求";
        public const string AsynSre = "1000:处理成功";

        #endregion

        #region 基础技术错误码

        public const string JsonDeserializeError = "E001:Json格式不正确";
        public const string CheckSignFail = "F001:验证签名失败";

        #endregion

        #region 基础业务错误码

        public const string OrganizationNull = "N001:未授权";

        #endregion
    }
}
