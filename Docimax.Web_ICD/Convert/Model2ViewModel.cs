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
        public static VerifyIdentityViewModel Model2VerifyIdentityModel(VerifyIdentityModel model)
        {
            return new VerifyIdentityViewModel
            {
                UserID = model.UserID,
                UserName = model.UserName,
                IDCardNo = ICD_EncryptUtils.DecryptByAES(model.IDCardNo),
                RealName = ICD_EncryptUtils.DecryptByAES(model.RealName),
                BankCardNO = ICD_EncryptUtils.DecryptByAES(model.BankCardNO),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                CertificateFlag = model.CertificateFlag,
                LastModifyStamp = model.LastModifyStamp,
            };
        }
    }
}