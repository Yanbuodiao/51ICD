using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        UserInfoModel GetUserInfo(string userID);
        ICDExcuteResult<int> ApplyIdentityVerify(VerifyIdentityModel model);
        ICDPagedList<UserCertificationSearch, VerifyIdentityModel> GetUserVerifyList(ICDPagedList<UserCertificationSearch, VerifyIdentityModel> queryModel);
        VerifyIdentityModel GetVerifyIdentityModel(string userID);
        ICDExcuteResult<int> AuditIdentityVerify(VerifyIdentityModel model);
        ICDExcuteResult<int> ApplyServiceProvider(UserServiceModel model);
        ICDPagedList<UserServiceSearch, UserServiceModel> GetUserServiceList(ICDPagedList<UserServiceSearch, UserServiceModel> queryModel);
        UserServiceModel GetUserService(int user_serviceID);
        ICDExcuteResult<int> AuditServiceProvider(UserServiceModel model);
    }
}
