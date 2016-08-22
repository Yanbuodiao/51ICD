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
        public static VerifyIdentityModel VerifyIdentityModel2Model(VerifyIdentityViewModel model)
        {
            return new VerifyIdentityModel
            {
                UserID = model.UserID,
                IDCardNo = ICD_EncryptUtils.EncryptByAES(model.IDCardNo),
                RealName = ICD_EncryptUtils.EncryptByAES(model.RealName),
                BankCardNO = ICD_EncryptUtils.EncryptByAES(model.BankCardNO),
                ApplyTime = model.ApplyTime,
                FileList = model.FileList,
                CertificateFlag = model.CertificateFlag,
            };
        }
    }
}