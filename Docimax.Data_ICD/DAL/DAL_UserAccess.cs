﻿using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System.Linq;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_UserAccess : IUserAccess
    {
        public ICDExcuteResult ApplyIdentityVerify(VerifyIdentityModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        return new ICDExcuteResult { Result = false, ErrorStr = "未获得当前用户信息", };
                    }
                    var applyInt = CertificateState.未认证.GetHashCode();
                    if (user.CertificationFlag != model.CertificateFlag.GetHashCode())
                    {
                        return new ICDExcuteResult { Result = false, ErrorStr = "已经发起过实名认证申请", };
                    }
                    user.CertificationFlag = model.CertificateFlag.GetHashCode();
                    user.RealName = model.RealName;
                    user.IDCardNo = model.IDCardNo;
                    user.BankCardNO = model.BankCardNO;
                    foreach (var item in model.FileList)
                    {
                        var userAttach = new User_Attach
                        {
                            UserID = model.UserID,
                            AttachType = item.AttachType,
                            ContentType = item.ContentType,
                            CreateTime = model.ApplyTime,
                            CreateUserID = model.UserID,
                            LastModifyUserID = model.UserID,
                            LastModityTime = model.ApplyTime,
                        };
                        entity.User_Attach.Add(userAttach);
                    }
                    entity.SaveChanges();
                    return new ICDExcuteResult { Result = true };
                }
            }
        }

        public UserInfoModel GetUserInfo(string userID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID);
                if (model == null)
                {
                    return new UserInfoModel();
                }
                return new UserInfoModel
                {
                    UserID = userID,
                    CertificationFlag = (CertificateState)(model.CertificationFlag ?? 0),
                    HasPassword = true,
                    PhoneNumber = model.PhoneNumber,
                    TwoFactor = model.TwoFactorEnabled,
                };
            }
        }
    }
}
