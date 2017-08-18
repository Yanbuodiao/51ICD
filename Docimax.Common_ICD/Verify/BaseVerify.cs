using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Message;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Web;

namespace Docimax.Common_ICD.Verify
{
    public class BaseVerify
    {
        public static ExcuteResult RequiredValidate<T>(UploadModel<T> model) where T : class
        {
            if (model == null)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.RequestNull,
                };
            }
            if (string.IsNullOrWhiteSpace(model.TicketID))
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.TicketNull,
                };
            }
            if (model.UploadContent == null)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.MedicalRecordNull,
                };
            }
            //todo 视情况验证回调地址的非空
            return new ExcuteResult
            {
                IsSuccess = true,
            };
        }

        public static ExcuteResult ValidateCodeOrder(MedicalRecordBase model)
        {
            if (model == null)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.MedicalRecordNull,
                };
            }
            if (string.IsNullOrWhiteSpace(model.PlatformOrderCode))//没有平台订单号，视作新建
            {
                if (string.IsNullOrWhiteSpace(model.MedicalRecordNO))
                {
                    return new ExcuteResult
                    {
                        IsSuccess = false,
                        ErrorStr = MessageStr.MedicalRecordNoNull,
                    };
                }
                if (model.DischargeDate == null || ((DateTime)model.DischargeDate).AddYears(30) < DateTime.Now)
                {
                    return new ExcuteResult
                    {
                        IsSuccess = false,
                        ErrorStr = MessageStr.DischargeTimeFail,
                    };
                }
                var tempkey = string.Format("{0}{1:yyyyMMdd}{2}", model.MedicalRecordNO, model.DischargeDate, model.AdmissionTimes);
                if (HttpRuntime.Cache[tempkey] != null)
                {
                    return new ExcuteResult { ErrorStr = MessageStr.MedicalRecordRepeat };
                }
                HttpRuntime.Cache.Insert(tempkey, 0, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 20));
                ICode_Order access = new DAL_Code_Order();
                if (access.IsCodeOrderExistByMecicalRecord(model))
                {
                    return new ExcuteResult { ErrorStr = MessageStr.MedicalRecordRepeat };
                }
                return new ExcuteResult { IsSuccess = true };
            }
            else//有平台订单号 视作更新
            {
                return new ExcuteResult { IsSuccess = true };
            }
        }
    }
}
