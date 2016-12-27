using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.Security;

namespace Docimax.Interface_ICD.Interface
{
    public interface ISecurity
    {
        bool NeedVerifyIP(SecurityIPModel model);

        SecurityPhoneModel GetLastPhoneMessage(string phoneNumber);

        void SavePhoneMessage(SecurityPhoneModel model);

        ICDExcuteResult<int> DymaticCodeVerify(string phoneNum, string sourceIP, int shouldDelaySecond);

        bool IsPhoneNumUnicity(string uid, string phoneNum);
    }
}
