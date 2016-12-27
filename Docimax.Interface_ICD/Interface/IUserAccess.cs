using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface
{
    public interface IUserAccess
    {
        UserInfoModel GetUserInfo(string userID);
        ICDExcuteResult<int> ApplyIdentityVerify(VerifyUserModel model);
        ICDExcuteResult<int> ApplyBankCardVerify(VerifyUserModel model);
        ICDTimePagedList<UserCertificationSearch, VerifyUserModel> GetUserVerifyList(ICDTimePagedList<UserCertificationSearch, VerifyUserModel> queryModel);
        ICDTimePagedList<UserCertificationSearch, VerifyUserModel> GetBankCardVerifyList(ICDTimePagedList<UserCertificationSearch, VerifyUserModel> queryModel);
        VerifyUserModel GetVerifyIdentityModel(string userID);
        ICDExcuteResult<int> AuditIdentityVerify(VerifyUserModel model);
        ICDExcuteResult<int> AuditBankCard(VerifyUserModel model);
        ICDExcuteResult<int> ApplyServiceProvider(UserServiceModel model);
        ICDTimePagedList<UserServiceSearch, UserServiceModel> GetUserServiceList(ICDTimePagedList<UserServiceSearch, UserServiceModel> queryModel);
        UserServiceModel GetUserService(int user_serviceID);
        ICDExcuteResult<int> AuditServiceProvider(UserServiceModel model);
        ICDExcuteResult<string> CreateOrLogin(string phoneNum, string hospitalName);
    }
}
