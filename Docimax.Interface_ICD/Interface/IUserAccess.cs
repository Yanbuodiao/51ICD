using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        ICDExcuteResult<int> ApplyIdentityVerify(VerifyIdentityModel model);
        UserInfoModel GetUserInfo(string userID);
        ICDExcuteResult<int> ApplyServiceProvider(UserServiceApplyModel model);

        ICDPagedList<UserCertificationSearch, VerifyIdentityModel> GetUpLoadedCodeOrderList(ICDPagedList<UserCertificationSearch, VerifyIdentityModel> queryModel);
    }
}
