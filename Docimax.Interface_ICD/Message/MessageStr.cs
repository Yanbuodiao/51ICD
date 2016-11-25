
namespace Docimax.Interface_ICD.Message
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
        /// <summary>
        /// E007:请求时间超时无效
        /// </summary>
        public const string OutTime = "E007:请求时间超时无效";
        /// <summary>
        /// 内部错误
        /// </summary>
        public const string InnerError = "EEEE:内部错误";

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
        /// <summary>
        /// B004:病案内容为空
        /// </summary>
        public const string MedicalRecordNull = "B004:病案内容为空";
        /// <summary>
        /// B005:病案号为空
        /// </summary>
        public const string MedicalRecordNoNull = "B005:病案号为空";
        /// <summary>
        /// B006:出院日期不合法
        /// </summary>
        public const string DischargeTimeFail = "B006:出院日期不合法";
        /// <summary>
        /// B007:病案重复
        /// </summary>
        public const string MedicalRecordRepeat = "B007:病案重复";
        /// <summary>
        ///B008:医疗机构未认证  机构未通过服务认证
        /// </summary>
        public const string OrganizationFail = "B008:医疗机构未认证";
        /// <summary>
        ///B009:服务未开通  机构未通过服务认证
        /// </summary>
        public const string ServiceFail = "B009:服务未开通";
        #endregion
    }
}
