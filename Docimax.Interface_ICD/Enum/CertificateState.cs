
namespace Docimax.Interface_ICD.Enum
{
    /// <summary>
    /// 实名/服务认证状态
    /// </summary>
    public enum CertificateState
    {
        未申请 = 10,
        发起认证申请 = 20,
        平台人员一次审核失败 = 55,//预留
        平台人员一次审核通过 = 100,//预留
        平台人员二次次审核失败 = 155,//预留
        平台人员二次审核通过 = 200,//预留
        认证拒绝 = 555,
        冻结 = 655,//预留
        认证通过 = 1000,
    }
}
