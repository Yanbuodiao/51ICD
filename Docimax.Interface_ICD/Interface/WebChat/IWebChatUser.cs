using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;

namespace Docimax.Interface_ICD.Interface.WebChat
{
    public interface IWebChatUser
    {
        void CreateOrUpdateUser(WebChatUserInfo webchatUserInfo);

        int GetDefaultICDVersion(string openid, ICDTypeEnum icdType);

        ICDExcuteResult<string> SetDefaultICDVersion(ICDTypeEnum icdType, string openid, int icdVersionID);
    }
}
