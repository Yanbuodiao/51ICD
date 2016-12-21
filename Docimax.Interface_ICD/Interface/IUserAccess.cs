using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        UserInfoModel GetUserInfo(string userID);
        ICDExcuteResult<int> ApplyIdentityVerify(VerifyIdentityModel model);
        ICDTimePagedList<UserCertificationSearch, VerifyIdentityModel> GetUserVerifyList(ICDTimePagedList<UserCertificationSearch, VerifyIdentityModel> queryModel);
        VerifyIdentityModel GetVerifyIdentityModel(string userID);
        ICDExcuteResult<int> AuditIdentityVerify(VerifyIdentityModel model);
        ICDExcuteResult<int> ApplyServiceProvider(UserServiceModel model);
        ICDTimePagedList<UserServiceSearch, UserServiceModel> GetUserServiceList(ICDTimePagedList<UserServiceSearch, UserServiceModel> queryModel);
        UserServiceModel GetUserService(int user_serviceID);
        ICDExcuteResult<int> AuditServiceProvider(UserServiceModel model);
        ICDExcuteResult<string> CreateOrLogin(string phoneNum, string hospitalName);
    }
}
