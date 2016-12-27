using Docimax.Common.Encryption;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docimax.Web_ICD.Convert
{
    public static class ViewModel2Model
    {
        public static VerifyUserModel VerifyIdentityModel2Model(VerifyIdentityViewModel model)
        {
            return new VerifyUserModel
            {
                UserID = model.UserID,
                UserName = model.UserName,
                IDCardNo = ICD_EncryptUtils.EncryptByAES(model.IDCardNo),
                RealName = ICD_EncryptUtils.EncryptByAES(model.RealName),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                CertificateFlag = model.CertificateFlag,
                LastModifyStamp = model.LastModifyStamp,
            };
        }

        public static VerifyUserModel VerifyIdentityModel2Model(VerifyBankCardModel model)
        {
            return new VerifyUserModel
            {
                UserID = model.UserID,
                BankCardNO = ICD_EncryptUtils.EncryptByAES(model.BankCardNO),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                BankCertificationFlag = model.CertificateFlag,
                LastModifyStamp = model.LastModifyStamp,
            };
        }
    }
}