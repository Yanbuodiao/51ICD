using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        ICDExcuteResult ApplyIdentityVerify(VerifyIdentityModel Model);
    }
}
