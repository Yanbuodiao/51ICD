using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface.WebChat;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Data_ICD.DAL.WebChat
{
    public class DAL_WebChatUser : IWebChatUser
    {
        public void CreateOrUpdateUser(WebChatUserInfo webchatUserInfo)
        {
            if (webchatUserInfo == null)
            {
                return;
            }
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var entityModel = entity.WebChat_User.FirstOrDefault(e => e.Openid == webchatUserInfo.openId);
                    if (entityModel != null)
                    {
                        entityModel.NikceName = webchatUserInfo.nickName;
                        entityModel.Country = webchatUserInfo.country;
                        entityModel.City = webchatUserInfo.city;
                        entityModel.AvatarUrl = webchatUserInfo.avatarUrl;
                        entityModel.Gender = webchatUserInfo.gender;
                        entityModel.Province = webchatUserInfo.province;
                        entityModel.LastLoginTime = DateTime.Now;
                    }
                    else
                    {
                        var newModel = new WebChat_User
                        {
                            Openid = webchatUserInfo.openId,
                            Appid = webchatUserInfo.watermark.appid,
                            AvatarUrl = webchatUserInfo.avatarUrl,
                            City = webchatUserInfo.city,
                            Country = webchatUserInfo.country,
                            Gender = webchatUserInfo.gender,
                            Unionid = webchatUserInfo.unionId,
                            NikceName = webchatUserInfo.nickName,
                            Province = webchatUserInfo.province,
                            LastLoginTime = DateTime.Now,
                        };
                        entity.WebChat_User.Add(newModel);
                    }
                    entity.SaveChanges();
                    trasanction.Commit();
                }
            }
        }

        public int GetDefaultICDVersion(string openid, ICDTypeEnum icdType)
        {
            using (var entity = new Entity_Read())
            {
                var typeInt = icdType.GetHashCode();
                var entityModel = entity.WebChat_UserICDVersion.FirstOrDefault(e => e.Openid == openid && e.ICD_Type == typeInt);
                if (entityModel != null)
                {
                    return entityModel.ICD_VersionID;
                }
                return -1;
            }
        }

        public ICDExcuteResult<string> SetDefaultICDVersion(ICDTypeEnum icdType, string openid, int icdVersionID)
        {
            using (var entity = new Entity_Write())
            {
                if (string.IsNullOrWhiteSpace(openid))
                {
                    return new ICDExcuteResult<string>
                    {
                        IsSuccess = false,
                        ErrorStr = "未获得微信用户ID",
                    };
                }
                var typeInt = icdType.GetHashCode();
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var entityModel = entity.WebChat_UserICDVersion.FirstOrDefault(e => e.Openid == openid && e.ICD_Type == typeInt);
                    if (entityModel != null)
                    {
                        if (icdVersionID > 0)
                        {
                            entityModel.ICD_VersionID = icdVersionID;
                        }
                        else
                        {
                            entity.WebChat_UserICDVersion.Remove(entityModel);
                        }
                    }
                    else
                    {
                        if (icdVersionID > 0)
                        {
                            var newModel = new WebChat_UserICDVersion
                            {
                                ICD_Type = typeInt,
                                ICD_VersionID = icdVersionID,
                                Openid = openid,
                            };
                            entity.WebChat_UserICDVersion.Add(newModel);
                        }
                    }
                    entity.SaveChanges();
                    trasanction.Commit();
                    return new ICDExcuteResult<string> { IsSuccess = true };
                }
            }
        }
    }
}
