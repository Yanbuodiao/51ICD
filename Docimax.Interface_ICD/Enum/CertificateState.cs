
namespace Docimax.Interface_ICD.Enum
{
    /// <summary>
    /// 实名/服务认证状态
    /// </summary>
    public enum CertificateState
    {
        未认证=0,
        发起认证申请=20,
        平台人员审核通过=40,
        认证成功=100,
        认证失败=555,
    }
}
