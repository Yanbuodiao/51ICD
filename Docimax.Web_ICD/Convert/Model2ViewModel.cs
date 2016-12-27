using Docimax.Common.Encryption;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docimax.Web_ICD.Convert
{
    public static class Model2ViewModel
    {
        public static VerifyIdentityViewModel Model2VerifyIdentityModel(VerifyUserModel model)
        {
            return new VerifyIdentityViewModel
            {
                UserID = model.UserID,
                UserName = model.UserName,
                IDCardNo = ICD_EncryptUtils.DecryptByAES(model.IDCardNo),
                RealName = ICD_EncryptUtils.DecryptByAES(model.RealName),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                CertificateFlag = model.CertificateFlag,
                LastModifyStamp = model.LastModifyStamp,
            };
        }

        public static VerifyBankCardModel Model2VerifyBankCardModel(VerifyUserModel model)
        {
            return new VerifyBankCardModel
            {
                UserID = model.UserID,
                UserName=model.UserName,
                BankCardNO = ICD_EncryptUtils.DecryptByAES(model.BankCardNO),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                CertificateFlag = model.BankCertificationFlag,
                LastModifyStamp = model.LastModifyStamp,
            };
        }
    }
}