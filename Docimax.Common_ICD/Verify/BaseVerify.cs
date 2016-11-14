using Docimax.Common_ICD.Message;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.UploadModel;
using System;

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
            if (model.UploadContent==null)
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

        public static ExcuteResult ValidateNewCodeOrder(MedicalRecordCoding model)
        {
            if (model == null)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.MedicalRecordNull,
                };
            }
            if (string.IsNullOrWhiteSpace(model.MedicalRecordNO))
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.MedicalRecordNoNull,
                };
            }
            if (model.DischargeDate==null||((DateTime)model.DischargeDate).AddYears(30)<DateTime.Now)
            {
                return new ExcuteResult
                {
                    IsSuccess = false,
                    ErrorStr = MessageStr.DischargeTimeFail,
                };
            }

            return new ExcuteResult
            {
                IsSuccess = true,
            };
        }
    }
}
