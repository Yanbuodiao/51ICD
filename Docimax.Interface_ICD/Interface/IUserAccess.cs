using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        ICDExcuteResult<int> ApplyIdentityVerify(VerifyIdentityModel Model);
        UserInfoModel GetUserInfo(string userID);
    }
}
